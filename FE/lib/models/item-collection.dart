import 'package:metal_collector/services/artist-service.dart';

class ItemCollection {
  final String? itemId;
  final String? artistId;
  final String? emId;
  final String name; // Requerido
  final String itemType; // Requerido
  final DateTime? buyDate;
  final String? barcode;
  final Artist? artists;

  ItemCollection({
    this.itemId,
    this.artistId,
    this.emId,
    required this.name,
    required this.itemType,
    this.buyDate,
    this.barcode,
    this.artists,
  });

  // Método para convertir un Map a una instancia de ItemCollection
  factory ItemCollection.fromJson(Map<dynamic, dynamic> json) {
    return ItemCollection(
      artistId: json['artistId'],
      itemId: json['itemId'],
      emId: json['em_id'],
      name: json['name'],
      itemType: json['itemType'],
      buyDate: json['buyDate'] != null ? DateTime.parse(json['buyDate']) : null,
      barcode: json['barcode'],
      artists: json['artists'] != null ? Artist.fromJson(json['artists']) : null,
    );
  }

  // Método para convertir una instancia de ItemCollection a un Map
  Map<String, dynamic> toMap() {
    return {
      'artistId': artistId,
      'em_id': emId,
      'name': name,
      'itemType': itemType,
      'buyDate': buyDate?.toIso8601String(),
      'barcode': barcode,
    };
  }

  factory ItemCollection.fromMap(Map<String, dynamic> map, String id) {
    return ItemCollection(
      artistId: map['artistId'],
      emId: map['em_id'],
      name: map['name'],
      itemType: map['itemType'],
      buyDate: map['buyDate'] != null ? DateTime.parse(map['buyDate']) : null,
      barcode: map['barcode'],
    );
  }
}
