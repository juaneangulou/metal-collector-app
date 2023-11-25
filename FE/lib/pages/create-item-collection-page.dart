import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:metal_collector/bottom-navigation-custom-widget.dart';

import '../widgets/create-item-collection-form-widget.dart';

class CreateItemCollectionPage extends StatelessWidget {
  /// Constructs a [CreateItemCollectionPage]
  const CreateItemCollectionPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      bottomNavigationBar: BottomNavigationCustomWidgetPage(),
      body: CreateItemCollectionFormWidget(),
      );
    
  }
}