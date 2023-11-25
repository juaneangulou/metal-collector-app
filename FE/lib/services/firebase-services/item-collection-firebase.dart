import 'package:cloud_firestore/cloud_firestore.dart';

import '../../models/dashboard-models.dart';
import '../../models/item-collection.dart';

class ItemCollectionFirebaseService {
  get collectionName => 'item-collection';

  Future<List<ItemCollection>> getAll() async {
    var query = FirebaseFirestore.instance.collection(collectionName);

    // if (search != null && search.isNotEmpty) {
    //   query = query.where('name', arrayContains: search);
    // }

    final result = await query.get();

    return result.docs
        .map((doc) => ItemCollection.fromMap(doc.data(), doc.id))
        .toList();
  }

  Future<ItemCollection> getById(String id) async {
    final doc = await FirebaseFirestore.instance
        .collection(collectionName)
        .doc(id)
        .get();

    return ItemCollection.fromMap(doc.data()!, doc.id);
  }

  Future<String> add(ItemCollection itemCollection) async {
    final collection = FirebaseFirestore.instance.collection(collectionName);

    // Step 1: Get a reference to a new document.
    final documentRef = collection.doc();

    // Step 2: The ID is accessible even before the document is created.
    final documentId = documentRef.id;

    // Step 3: Use the reference to set the data.
    await documentRef.set(itemCollection!.toMap());
    return documentId;
  }

  Future<void> update(ItemCollection itemCollection, String id) async {
    await FirebaseFirestore.instance
        .collection(collectionName)
        .doc(id)
        .update(itemCollection.toMap());
  }

  Future<void> delete(String id) async {
    await FirebaseFirestore.instance
        .collection(collectionName)
        .doc(id)
        .delete();
  }

  Future<List<DashboadModel>?> getDashboardData() async {
    List<DashboadModel> response = []; // 1. Fix the initialization here
    const itemCollectionType = ['CD', 'Types', 'DVD', 'Vinyl', 'VCD'];
    for (var i = 0; i < itemCollectionType.length; i++) {
      var result = await FirebaseFirestore.instance
          .collection(collectionName)
          .where('itemType', isEqualTo: itemCollectionType[i])
          .get(); // 2. Fix this line to get documents

      if (result.docs.length > 0) {
        // Count the documents
        response.add(DashboadModel(
            CollectionType: itemCollectionType[i],
            Count: result.docs.length // Count the documents
            ));
      }
    }
    return response;
  }
}
