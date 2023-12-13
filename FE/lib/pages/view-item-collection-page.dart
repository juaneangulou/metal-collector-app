import 'package:flutter/material.dart';
import 'package:metal_collector/bottom-navigation-custom-widget.dart';
import 'package:metal_collector/models/item-collection.dart';
import 'package:metal_collector/services/artist-service.dart';
import 'package:metal_collector/services/firebase-services/artist-firebase-services.dart';
import 'package:metal_collector/services/firebase-services/items-metal-collection-services.dart';
// Asegúrate de importar tus otros archivos y modelos necesarios aquí
import 'package:metal_collector/widgets/artist-basic-card-widget.dart';

class ItemCollectionScreen extends StatefulWidget {
  @override
  _ItemCollectionScreenState createState() => _ItemCollectionScreenState();
}

class _ItemCollectionScreenState extends State<ItemCollectionScreen> {
  List<ItemCollection> allCollections = [];
  List<ItemCollection> displayedCollections = [];

  List<Artist> artists = [];
  Artist? selectedArtist;

  final serviceArtist = ArtistFirebaseService();
  final serviceItemCollection = ItemMetalCollectorService();

  @override
  void initState() {
    super.initState();
    fetchData();
  }

  Future<void> fetchData() async {
    allCollections = await serviceItemCollection.fetchItems();

    setState(() {
      displayedCollections = List.from(allCollections);
    });
  }

  void filterByArtist() {
    // Tu lógica para filtrar por artista...
  }

  void clearFilter() {
    // Tu lógica para limpiar el filtro...
  }

   deleteItem(String itemId) async {
    // Aquí puedes añadir tu lógica para eliminar un ítem
   await serviceItemCollection.deleteItem(itemId); // Llama a tu servicio para eliminar el ítem
   fetchData();
    // Luego, actualiza el estado para reflejar la eliminación
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Item Collections")),
      body: Column(
        children: [
          // Tu widget de Autocomplete y botón de limpiar filtro...

          Expanded(
            child: ListView.builder(
              itemCount: displayedCollections.length,
              itemBuilder: (context, index) {
                final item = displayedCollections[index];
                final artist = displayedCollections[index].artists; // Asegúrate de que esta línea obtenga correctamente el artista

                return Card(
                  elevation: 4,
                  margin: EdgeInsets.all(8),
                  child: Column(
                    children: [
                      ListTile(
                        title: Text(item.name),
                        subtitle: Text(item.itemType),
                        trailing: IconButton(
                          icon: Icon(Icons.delete),
                          onPressed: () async => deleteItem(item.itemId!), // Llama a deleteItem cuando se presione el ícono
                        ),
                      ),
                      if (artist != null)
                        Padding(
                          padding: EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                          child: ArtistCard(artist: artist),
                        ),
                    ],
                  ),
                );
              },
            ),
          ),
        ],
      ),
      bottomNavigationBar: BottomNavigationCustomWidgetPage(),
    );
  }
}
