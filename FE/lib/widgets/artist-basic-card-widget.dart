import 'package:flutter/material.dart';

import '../services/artist-service.dart';

class ArtistCard extends StatelessWidget {
  final Artist artist;

  ArtistCard({required this.artist});

  @override
  Widget build(BuildContext context) {
    return Card(
      elevation: 5,
      margin: EdgeInsets.symmetric(vertical: 10, horizontal: 20),
      child: Row(
        children: [
          // Imagen circular
          Padding(
            padding: EdgeInsets.all(10),
            child: CircleAvatar(
              backgroundImage: artist.photo != null ? NetworkImage(artist.photo!) : null,
              radius: 40, // Puedes ajustar este valor según prefieras
            ),
          ),
          
          // Detalles del artista
          Expanded(
            child: Padding(
              padding: EdgeInsets.all(10),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(artist.name, style: TextStyle(fontWeight: FontWeight.bold)),
                  SizedBox(height: 5),
                  Text(artist.genre ?? ''),
                  SizedBox(height: 5),
                  Text(artist.countryName ?? ''),
                  SizedBox(height: 5),
                  Text(artist.location ?? ''),
                  SizedBox(height: 5),
                  Text('Formed: ${artist.formed ?? ''}'),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}