import 'dart:convert';

import 'package:dio/dio.dart';
import 'package:metal_collector/models/item-collection.dart';

class ItemMetalCollectorService {
  final Dio _dio = Dio();

  final String baseUrl =
      'https://api-metalcollector.mymetalevents.com/api/Items';

  Future<List<ItemCollection>> searchItems(String query) async {
    try {
      final response =
          await _dio.get(baseUrl, queryParameters: {'query': query});
      if (response.statusCode == 200) {
        List data = response.data;
        return data.map((item) => ItemCollection.fromJson(item)).toList();
      } else {
        throw Exception('Failed to fetch items');
      }
    } catch (e) {
      print(e);
      throw Exception('Failed to fetch items');
    }
  }

  Future<List<ItemCollection>> fetchItems() async {
    try {
      final response = await _dio
          .get('https://api-metalcollector.mymetalevents.com/api/Items');
      if (response.statusCode == 200) {
        List data = response.data;
        return data.map((item) => ItemCollection.fromJson(item)).toList();
      } else {
        throw Exception('Failed to load items');
      }
    } catch (e) {
      print(e);
      throw Exception('Failed to load items');
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
