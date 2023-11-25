import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';


class BottomNavigationCustomWidgetPage extends StatefulWidget {
  const BottomNavigationCustomWidgetPage( {super.key});
  @override
  _BottomNavigationCustomWidgetPageState createState() => _BottomNavigationCustomWidgetPageState();
}

class _BottomNavigationCustomWidgetPageState extends State<BottomNavigationCustomWidgetPage> {
  int _currentIndex = 0; // Índice actual

  // Páginas o vistas para cada tab
  final List<Widget> _pages = [
    const Center(child: Text('Tab 1')),
    const Center(child: Text('Tab 2')),
    const Center(child: Text('Tab 3')),
    const Center(child: Text('Tab 4')),
  ];

  @override
  Widget build(BuildContext context) {
    return  BottomNavigationBar(
      type: BottomNavigationBarType.fixed,
        currentIndex: _currentIndex,
        onTap: (index) {
          setState(() {
            _currentIndex = index;
          });
          switch(index){
            case 0:
               context.go('/home',extra: {});
              break;
            case 1:
               context.go('/view-item-collection-all',extra: {});
              break;
            case 2:
              context.go('/create-item-collection');
              break;
            case 3:
              Navigator.pushNamed(context, '/profile');
              break;
          }
        },
        items: const [
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: 'Inicio',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.search),
            label: 'Ver colección',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.add),
            label: 'Agregar elemento',

          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.person),
            label: 'Perfil',
          ),
        ],
      );
  }
}