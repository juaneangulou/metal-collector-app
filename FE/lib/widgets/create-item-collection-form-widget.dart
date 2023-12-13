import 'dart:async';

import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:flutter/material.dart';
import 'package:metal_collector/services/firebase-services/item-collection-firebase.dart';

import '../models/item-collection.dart';
import '../services/artist-service.dart';
import '../services/firebase-services/artist-firebase-services.dart';
import 'artist-basic-card-widget.dart';

class CreateItemCollectionFormWidget extends StatefulWidget {
  @override
  _CreateItemCollectionFormWidgetState createState() =>
      _CreateItemCollectionFormWidgetState();
}

class _CreateItemCollectionFormWidgetState
    extends State<CreateItemCollectionFormWidget> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _dateController = TextEditingController();
  final TextEditingController _barcodeController = TextEditingController();
  final TextEditingController _artistController = TextEditingController();
  final _articleNameController = TextEditingController();

  Artist? _selectedArtistDetails;

  Timer? _debounce;
  List<Artist> _artistOptions = [];

  String? _selectedType;
  String? _selectedArtist;
  DateTime? _buyDate;

  @override
  void initState() {
    super.initState();
    _dateController.text = ""; // Inicializamos el campo de fecha como vacío
  }

  @override
  void dispose() {
    _dateController.dispose();
    _artistController.dispose();
    _barcodeController.dispose();
    _debounce?.cancel();
    super.dispose();
  }

  _onSearchChanged(String query) {
    if (_debounce?.isActive ?? false) _debounce?.cancel();
    _debounce = Timer(const Duration(milliseconds: 500), () async {
      // Llamamos a la función para buscar artistas una vez que el usuario haya dejado de teclear
      _artistOptions = await fetchArtists(query);
      setState(() {});
    });
  }

  void _showDiscographyModal(BuildContext context) {
    showModalBottomSheet(
      context: context,
      builder: (context) {
        return ListView.builder(
          itemCount: (_selectedArtistDetails?.discography?.length ?? 0) + 1,
          itemBuilder: (context, index) {
            if (index == 0) {
              return ListTile(
                title: Text('Cancelar'),
                leading: Icon(Icons.close),
                onTap: () {
                  Navigator.of(context).pop();
                },
              );
            }
            Discography disc = _selectedArtistDetails!.discography![index - 1];
            return ListTile(
              title: Text('${disc.title} - ${disc.year}'),
              onTap: () {
                setState(() {
                  _articleNameController.text = '${disc.title} - ${disc.year}';
                });
                Navigator.of(context).pop();
              },
            );
          },
        );
      },
    );
  }

  _getArtistDetails(String id) async {
    // Llamamos a la función para buscar artistas una vez que el usuario haya dejado de teclear
    final artistDetail = await fetchArtistById(id);
    setState(() {
      _selectedArtistDetails = artistDetail;
    });
    if (artistDetail!.discography != null &&
        artistDetail.discography!.isNotEmpty) {
      _showDiscographyModal(context);
    }
  }

  void _clearArtistSelection() {
    setState(() {
      _artistController.clear();
      _selectedArtistDetails = null;
    });
  }

  void _showAlertmessage(String titulo, String mensaje) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(titulo),
          content: Text(mensaje),
          actions: <Widget>[
            TextButton(
              child: Text('Close'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  Future<void> _selectDate(BuildContext context) async {
    DateTime? pickedDate = await showDatePicker(
      context: context,
      initialDate: DateTime.now(),
      firstDate: DateTime(2000),
      lastDate: DateTime.now(),
    );
    if (pickedDate != null)
      setState(() {
        _dateController.text = pickedDate.toLocal().toString().split(' ')[0];
      });

    _buyDate = pickedDate;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Formulario con DatePicker y Validaciones'),
      ),
      body: Form(
        key: _formKey,
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: ListView(
            children: [
              Autocomplete<Artist>(
                optionsBuilder: (TextEditingValue textEditingValue) {
                  if (textEditingValue.text == '') {
                    return const Iterable<Artist>.empty();
                  }
                  return _artistOptions; // Simplemente devuelve las opciones actuales aquí
                },
                displayStringForOption: (Artist option) =>
                    '${option.name} - ${option.countryName} (${option.genre})',
                fieldViewBuilder: (BuildContext context,
                    TextEditingController fieldTextEditingController,
                    FocusNode fieldFocusNode,
                    VoidCallback onFieldSubmitted) {
                  return Row(
                    children: [
                      Expanded(
                        child: TextFormField(
                          controller: fieldTextEditingController,
                          focusNode: fieldFocusNode,
                          decoration: InputDecoration(
                            labelText: 'Select Artist',
                            hintText: 'Type to search...',
                          ),
                          onChanged: _onSearchChanged,
                        ),
                      ),
                      if (fieldTextEditingController.text.isNotEmpty)
                        IconButton(
                            icon: Icon(Icons.clear),
                            onPressed: () {
                              _clearArtistSelection();
                              fieldTextEditingController.clear();
                            }),
                    ],
                  );
                },
                onSelected: (Artist selection) async {
                  await _getArtistDetails(selection.id);
                  _artistController.text =
                      '${selection.name} - ${selection.countryName} (${selection.genre})';
                },
              ),
              if (_selectedArtistDetails != null)
                ArtistCard(artist: _selectedArtistDetails!),
              SizedBox(height: 20),
              TextFormField(
                controller: _articleNameController,
                decoration: InputDecoration(
                  labelText: "Article Name",
                  hintText: "Enter the article name",
                ),
                validator: (value) {
                  if (value!.isEmpty) {
                    return "Please enter the article name";
                  }
                  return null;
                },
              ),
              SizedBox(height: 20),
              DropdownButtonFormField(
                items: ['CD', 'Types', 'DVD', 'Vinyl', 'VCD']
                    .map((label) => DropdownMenuItem(
                          child: Text(label),
                          value: label,
                        ))
                    .toList(),
                hint: Text('Select Type'),
                onChanged: (value) {
                  setState(() {
                    _selectedType = value as String?;
                  });
                },
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return 'Please select a Type';
                  }
                  return null;
                },
              ),
              SizedBox(height: 20),
              TextFormField(
                controller: _dateController,
                decoration: InputDecoration(
                  labelText: 'Select Buy Date',
                  suffixIcon: IconButton(
                    icon: Icon(Icons.calendar_today),
                    onPressed: () => _selectDate(context),
                  ),
                ),
                onTap: () => _selectDate(context),
                readOnly: true,
              ),
              SizedBox(height: 20),
              TextFormField(
                controller: _barcodeController,
                decoration: InputDecoration(labelText: 'BarCode'),
              ),
              SizedBox(height: 20),
              ElevatedButton(
                onPressed: () async {
                  var artistId = "";

                  try {
                    final artistService = ArtistFirebaseService();
                    final itemCollectionService =
                        ItemCollectionFirebaseService();
                    if (_formKey.currentState!.validate()) {
                      _formKey.currentState!.save();
                      // final item=FirebaseFirestore.instance.collection('artists').add(_selectedArtistDetails!.toMap());
                      // print(item);
                      // final checkArtistInDb =
                      //     await ArtistFirebaseService().getByEMId(
                      //    _selectedArtistDetails!.emId!,
                      // );

                      // if ((checkArtistInDb!.length ?? 0) > 0) {
                      // } else {
                      //   artistId =
                      //       await artistService.add(_selectedArtistDetails!);
                      // }

                      // final artistUpdated =
                      //     await artistService.getById(artistId!);
                      ItemCollection newItem = ItemCollection(
                        artistId: _selectedArtistDetails!.id,
                        emId: _selectedArtistDetails!.emId,
                        name: _articleNameController.value.text,
                        itemType: _selectedType!,
                        buyDate: _buyDate,
                        barcode: _barcodeController.value.text,
                      );
                      final itemCollectionId =
                          itemCollectionService.add(newItem);
                    }
                    _showAlertmessage(
                        'Success', 'The operation was successful!');
                  } catch (e) {
                    _showAlertmessage('Error', 'An error occurred: $e');
                  }
                },
                child: Text('Submit'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
