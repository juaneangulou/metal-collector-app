import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

import '../bottom-navigation-custom-widget.dart';
import '../models/dashboard-models.dart';
import '../services/firebase-services/item-collection-firebase.dart';
import '../widgets/item-dashboard-basic-card-widget.dart';

class HomeScreen extends StatefulWidget {
  @override
  _HomeScreenState createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  final service = ItemCollectionFirebaseService();
  late Future<List<DashboadModel>?> dashboardListFuture;

  @override
  void initState() {
    super.initState();
    _loadData();  // Carga los datos cuando se inicializa el widget
  }

  _loadData() {
    dashboardListFuture = service.getDashboardData();  // Asigna el Future a una variable
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home Screen')),
      body: FutureBuilder<List<DashboadModel>?>(
        future: dashboardListFuture,  // Usa el Future que has creado
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return CircularProgressIndicator();  // Muestra un indicador de carga mientras se espera
          } else if (snapshot.hasError) {
            return Text('Error: ${snapshot.error}');  // Muestra un error si lo hay
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return Text('No data available');  // Muestra un mensaje si no hay datos
          } else {
            return ListView.builder(  // Crea una lista de DashboardCard
              itemCount: snapshot.data!.length,
              itemBuilder: (context, index) {
                var dashboardItem = snapshot.data![index];
                return DashboardCard(
                  // Asumiendo que DashboardCard toma un DashboadModel como parámetro
                  dashboadModel: dashboardItem,
                );
              },
            );
          }
        },
      ),
      bottomNavigationBar: const BottomNavigationCustomWidgetPage(),
    );
  }
}