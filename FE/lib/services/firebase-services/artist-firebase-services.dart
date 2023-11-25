import 'package:cloud_firestore/cloud_firestore.dart';

import '../../models/item-collection.dart';
import '../artist-service.dart';

class ArtistFirebaseService {
  get collectionName => 'artists';

  Future<List<Artist>> getAll() async {
    var query = FirebaseFirestore.instance.collection(collectionName);

    // if (search != null && search.isNotEmpty) {
    //   query = query.where('name', arrayContains: search);
    // }

    final result = await query.get();

    return result.docs
        .map((doc) => Artist.fromMap(doc.data(), doc.id))
        .toList();
  }

  Future<Artist> getById(String id) async {
    final doc = await FirebaseFirestore.instance
        .collection(collectionName)
        .doc(id)
        .get();

    return Artist.fromMap(doc.data()!, doc.id);
  }

  Future<String> add(Artist artist) async {
    final collection = FirebaseFirestore.instance.collection('artists');

    // Step 1: Get a reference to a new document.
    final documentRef = collection.doc();

    // Step 2: The ID is accessible even before the document is created.
    final documentId = documentRef.id;

    // Step 3: Use the reference to set the data.
    await documentRef.set(artist!.toMap());
    return documentId;
  }

  Future<void> update(Artist Artist) async {
    await FirebaseFirestore.instance
        .collection(collectionName)
        .doc(Artist.id)
        .update(Artist.toMap());
  }

  Future<void> delete(String id) async {
    await FirebaseFirestore.instance
        .collection(collectionName)
        .doc(id)
        .delete();
  }

  Future<List<Artist>> getByEMId(String emId) async {
    // var query = FirebaseFirestore.instance.collection(collectionName);

    // if (search != null && search.isNotEmpty) {
    //   query = query.where('name', arrayContains: search);
    // }
    final query = FirebaseFirestore.instance
        .collection(collectionName)
        .where('em_id', isEqualTo: emId);
    final result = await query.get();

    return result.docs
        .map((doc) => Artist.fromMap(doc.data(), doc.id))
        .toList();
  }
}
