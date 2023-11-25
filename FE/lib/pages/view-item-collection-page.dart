import 'package:flutter/material.dart';
import 'package:metal_collector/services/firebase-services/artist-firebase-services.dart';
import 'package:metal_collector/services/firebase-services/item-collection-firebase.dart';

import '../bottom-navigation-custom-widget.dart';
import '../models/item-collection.dart';
import '../services/artist-service.dart';

class ItemCollectionScreen extends StatefulWidget {
  @override
  _ItemCollectionScreenState createState() => _ItemCollectionScreenState();
}

class _ItemCollectionScreenState extends State<ItemCollectionScreen> {
  List<ItemCollection> allCollections = []; // Lista completa de item collections
  List<ItemCollection> displayedCollections = []; // Lista filtrada de item collections

  List<Artist> artists = []; // Lista completa de artistas
  Artist? selectedArtist; // Artista seleccionado en el autocompletado
final serviceArtist = ArtistFirebaseService();
    final serviceItemCollection = ItemCollectionFirebaseService();
  @override
  void initState()  {
    super.initState();
    // Aquí puedes llamar a tu método para obtener todos los item collections y artistas.
    // Por ahora, lo voy a dejar vacío.
    

    fetchData();  // Llamar a un método separado
}

Future<void> fetchData() async {
  // Tu lógica de obtención de datos va aquí. Por ejemplo:
    allCollections = await serviceItemCollection.getAll();

  setState(() {
    displayedCollections = List.from(allCollections); // Inicialmente, mostramos todas las colecciones
  });
}

  void filterByArtist() {
    if (selectedArtist == null) {
      displayedCollections = List.from(allCollections);
    } else {
      displayedCollections = allCollections.where((item) => item.artistId == selectedArtist!.emId).toList();
    }
    setState(() {});
  }

  void clearFilter() {
    selectedArtist = null;
    displayedCollections = List.from(allCollections);
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Item Collections")),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Row(
              children: [
                Expanded(
                  child: Autocomplete<Artist>(
                    optionsBuilder: (TextEditingValue textEditingValue) {
                      if (textEditingValue.text == '') {
                        return const Iterable<Artist>.empty();
                      }
                      return artists.where((Artist artist) => 
                        artist.name.contains(textEditingValue.text)
                      );
                    },
                    onSelected: (Artist selection) {
                      selectedArtist = selection;
                      filterByArtist();
                    },
                    displayStringForOption: (Artist option) => option.name,
                  ),
                ),
                IconButton(
                  icon: Icon(Icons.clear),
                  onPressed: clearFilter,
                )
              ],
            ),
          ),
          Expanded(
            child: ListView.builder(
              itemCount: displayedCollections.length,
              itemBuilder: (context, index) {
                final item = displayedCollections[index];
                return Card(
                  child: ListTile(
                    title: Text(item.name),
                    subtitle: Text(item.itemType),
                    // Puedes agregar más detalles aquí.
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
