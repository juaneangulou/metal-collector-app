import 'package:flutter/material.dart';
import 'package:metal_collector/models/dashboard-models.dart';

import '../services/artist-service.dart';

class DashboardCard extends StatelessWidget {
  final DashboadModel dashboadModel;

  DashboardCard({required this.dashboadModel});

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
            child: Icon(
              Icons.my_library_music,        // The name of the icon, in this case, a star
              size: 50.0        // The size of the icon
            ),
          ),
          
          // Detalles del artista
          Expanded(
            child: Padding(
              padding: EdgeInsets.all(10),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(dashboadModel.CollectionType??'', style: const TextStyle(fontWeight: FontWeight.bold)),
                  SizedBox(height: 5),
                  Text(dashboadModel.Count!.toString() ?? '')                 
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}