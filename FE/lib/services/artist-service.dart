import 'dart:convert';
import 'package:http/http.dart' as http;

class Artist {
  final String id;
  final String? emId;
  final String name;
  final String? genre;
  final String? countryCode;
  final String? countryName;
  final String? location;
  final String? formed;
  final String? active;
  final String? label;
  final String? photo;
  final String? link;
  final String? status;
  final List<Discography>? discography;
  final List<Lineup>? lineup;
  final List<Social>? social;

  Artist({
    required this.id,
    this.emId,
    required this.name,
    this.genre,
    this.countryCode,
    this.countryName,
    this.location,
    this.formed,
    this.active,
    this.label,
    this.photo,
    this.link,
    this.status,
    this.discography,
    this.lineup,
    this.social,
  });

  factory Artist.fromJson(Map<String, dynamic> json) {
    return Artist(
      id: json['id'],
      emId: json['em_id'],
      name: json['name'],
      genre: json['genre'],
      countryCode: json['country_code'],
      countryName: json['country_name'],
      location: json['location'],
      formed: json['formed'],
      active: json['active'],
      label: json['label'],
      photo: json['photo'],
      link: json['link'],
      status: json['status'],
      discography: json['discography'] != null ? 
                 (json['discography'] as List).map((e) => Discography.fromJson(e)).toList() : 
                 null, // Manejo cuando 'discography' no está presente.
      lineup: json['lineup'] != null ? 
                 (json['lineup'] as List).map((e) => Lineup.fromJson(e)).toList() : 
                 null, // Manejo cuando 'lineup' no está presente.
      social: json['social'] != null ? 
                 (json['social'] as List).map((e) => Social.fromJson(e)).toList() : 
                 null, // Manejo cuando 'social' no está presente.
    );
  }

  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'em_id': emId,
      'name': name,
      'genre': genre,
      'country_code': countryCode,
      'country_name': countryName,
      'location': location,
      'formed': formed,
      'active': active,
      'label': label,
      'photo': photo,
      'link': link,
      'status': status,
      'discography': discography?.map((e) => e.toMap()).toList(),
      'lineup': lineup?.map((e) => e.toMap()).toList(),
      'social': social?.map((e) => e.toMap()).toList(),
    };
  }

  factory Artist.fromMap(Map<String, dynamic> map, String id) {
    return Artist(
      id: map['id'],
      emId: map['em_id'],
      name: map['name'],
      genre: map['genre'],
      countryCode: map['country_code'],
      countryName: map['country_name'],
      location: map['location'],
      formed: map['formed'],
      active: map['active'],
      label: map['label'],
      photo: map['photo'],
      link: map['link'],
      status: map['status'],
      discography: map['discography'] != null ? 
                 (map['discography'] as List).map((e) => Discography.fromJson(e)).toList() : 
                 null, // Manejo cuando 'discography' no está presente.
      lineup: map['lineup'] != null ? 
                 (map['lineup'] as List).map((e) => Lineup.fromJson(e)).toList() : 
                 null, // Manejo cuando 'lineup' no está presente.
      social: map['social'] != null ? 
                 (map['social'] as List).map((e) => Social.fromJson(e)).toList() : 
                 null, // Manejo cuando 'social' no está presente.
    );
  }

}

class Discography {
  final String? title;
  final String? type;
  final String? year;

  Discography({this.title, this.type, this.year});

  factory Discography.fromJson(Map<String, dynamic> json) {
    return Discography(
      title: json['title'],
      type: json['type'],
      year: json['year'],
    );
  }

  Map<String, dynamic> toMap() {
    return {
      'title': title,
      'type': type,
      'year': year,
    };
  }
}

class Lineup {
  final String? id;
  final String? name;
  final String? instrument;

  Lineup({this.id, this.name, this.instrument});

  factory Lineup.fromJson(Map<String, dynamic> json) {
    return Lineup(
      id: json['id'],
      name: json['name'],
      instrument: json['instrument'],
    );
  }

  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'name': name,
      'instrument': instrument,
    };
  }
}

class Social {
  final String? media;
  final String? url;

  Social({this.media, this.url});

  factory Social.fromJson(Map<String, dynamic> json) {
    return Social(
      media: json['media'],
      url: json['url'],
    );
  }

  Map<String, dynamic> toMap() {
    return {
      'media': media,
      'url': url,
    };
  }
}


String processQueryString(String query) {
  return query.replaceAll(RegExp(r'\s{2,}'), ' ').replaceAll(' ', '*');
}

Future<List<Artist>> fetchArtists(String query) async {
  String processedQuery = processQueryString(query);
  final response = await http.get(Uri.parse('https://api.metal-map.com/v1/search/$processedQuery'));

  if (response.statusCode == 200) {
    List data = json.decode(response.body);
    return data.map((item) => Artist.fromJson(item)).toList();
  } else {
    return [];
    //throw Exception('Failed to load artists');
  }
}


Future<Artist?> fetchArtistById(String id) async {
  final response = await http.get(Uri.parse('https://api.metal-map.com/v1/bands/$id'));

  if (response.statusCode == 200) {
    List data = json.decode(response.body);
    return data.map((item) => Artist.fromJson(item)).toList().firstOrNull;
  } else {
    return null;
    //throw Exception('Failed to load artists');
  }
}