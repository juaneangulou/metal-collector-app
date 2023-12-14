import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:dio/dio.dart';

import '../../models/dashboard-models.dart';
import '../../models/item-collection.dart';
import 'items-metal-collection-services.dart';

class ItemCollectionFirebaseService {
  final Dio _dio = Dio();

  get collectionName => 'item-collection';
  final ItemMetalCollectorService _metalCollectorService =
      ItemMetalCollectorService();
  final String baseUrl =
      'https://api-metalcollector.mymetalevents.com/api/Items';

  Future<List<dynamic>> searchItems(String query) async {
    try {
      final response =
          await _dio.get(baseUrl, queryParameters: {'query': query});
      if (response.statusCode == 200) {
        return response.data;
      } else {
        throw Exception('Failed to fetch items');
      }
    } catch (e) {
      print(e);
      throw Exception('Failed to fetch items');
    }
  }

  Future<List<ItemCollection>> getAll() async {
    try {
      final response = await _dio
          .get('https://api-metalcollector.mymetalevents.com/api/Items');
      if (response.statusCode == 200) {
        return response.data;
      } else {
        throw Exception('Failed to load items');
      }
    } catch (e) {
      print(e);
      throw Exception('Failed to load items');
    }
  }

  Future<ItemCollection> getById(String id) async {
    final doc = await FirebaseFirestore.instance
        .collection(collectionName)
        .doc(id)
        .get();

    return ItemCollection.fromMap(doc.data()!, doc.id);
  }

  Future<String> add(ItemCollection itemCollection) async {
    // final collection = FirebaseFirestore.instance.collection(collectionName);

    // // Step 1: Get a reference to a new document.
    // final documentRef = collection.doc();

    // // Step 2: The ID is accessible even before the document is created.
    // final documentId = documentRef.id;

    // // Step 3: Use the reference to set the data.
    // await documentRef.set(itemCollection!.toMap());
    // return documentId;

    try {
      final response = await _dio.post(
        'https://api-metalcollector.mymetalevents.com/api/Items', // Asegúrate de tener la URL correcta
        data: itemCollection.toMap(), // Convierte el objeto a un Map
        options: Options(
          headers: {
            'accept': '*/*',
            'Content-Type': 'application/json',
          },
        ),
      );

      if (response.statusCode == 200 || response.statusCode == 201) {
        // Aquí necesitas determinar cómo obtener el ID del elemento agregado.
        // Esto dependerá de la respuesta del servidor.
        // Por ejemplo, si el servidor devuelve el ID en la respuesta:
        return response.data['itemId'];
      } else {
        throw Exception('Failed to add item');
      }
    } catch (e) {
      print(e);
      throw Exception('Failed to add item');
    }
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
    // List<DashboadModel> response = []; // 1. Fix the initialization here
    // const itemCollectionType = ['CD', 'Types', 'DVD', 'Vinyl', 'VCD'];
    // for (var i = 0; i < itemCollectionType.length; i++) {
    //   var result = await FirebaseFirestore.instance
    //       .collection(collectionName)
    //       .where('itemType', isEqualTo: itemCollectionType[i])
    //       .get(); // 2. Fix this line to get documents

    //   if (result.docs.length > 0) {
    //     // Count the documents
    //     response.add(DashboadModel(
    //         CollectionType: itemCollectionType[i],
    //         Count: result.docs.length // Count the documents
    //         ));
    //   }
    // }
    // return response;

    List<DashboadModel> response = [];
    const itemCollectionType = ['CD', 'Types', 'DVD', 'Vinyl', 'VCD'];

    try {
      final items = await _metalCollectorService.fetchItems();
      for (var type in itemCollectionType) {
        int count = items.where((item) => item.itemType == type).length;
        if (count > 0) {
          // Creando un mapa para pasar al constructor de DashboadModel
          Map<dynamic, Object> map = {'CollectionType': type, 'Count': count};
          response.add(DashboadModel(map, CollectionType: type, Count: count));
        }
      }
      return response;
    } catch (e) {
      print(e);
      return null;
    }
  }

  Future<bool> deleteItem(String itemId) async {
    try {
      final response = await _dio.delete(
          'https://api-metalcollector.mymetalevents.com/api/Items/$itemId');
      if (response.statusCode == 200) {
        // Asumiendo que una respuesta 200 indica éxito
        return true;
      } else {
        // Manejar diferentes códigos de estado o errores
        return false;
      }
    } catch (e) {
      print(e);
      return false; // Devuelve false en caso de excepción
    }
  }
}
