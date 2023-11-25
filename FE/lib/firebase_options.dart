// File generated by FlutterFire CLI.
// ignore_for_file: lines_longer_than_80_chars, avoid_classes_with_only_static_members
import 'package:firebase_core/firebase_core.dart' show FirebaseOptions;
import 'package:flutter/foundation.dart'
    show defaultTargetPlatform, kIsWeb, TargetPlatform;

/// Default [FirebaseOptions] for use with your Firebase apps.
///
/// Example:
/// ```dart
/// import 'firebase_options.dart';
/// // ...
/// await Firebase.initializeApp(
///   options: DefaultFirebaseOptions.currentPlatform,
/// );
/// ```
class DefaultFirebaseOptions {
  static FirebaseOptions get currentPlatform {
    if (kIsWeb) {
      return web;
    }
    switch (defaultTargetPlatform) {
      case TargetPlatform.android:
        return android;
      case TargetPlatform.iOS:
        return ios;
      case TargetPlatform.macOS:
        return macos;
      case TargetPlatform.windows:
        throw UnsupportedError(
          'DefaultFirebaseOptions have not been configured for windows - '
          'you can reconfigure this by running the FlutterFire CLI again.',
        );
      case TargetPlatform.linux:
        throw UnsupportedError(
          'DefaultFirebaseOptions have not been configured for linux - '
          'you can reconfigure this by running the FlutterFire CLI again.',
        );
      default:
        throw UnsupportedError(
          'DefaultFirebaseOptions are not supported for this platform.',
        );
    }
  }

  static const FirebaseOptions web = FirebaseOptions(
    apiKey: 'AIzaSyC7LND8jPcMAJ4IzL5pU3Tr4iRLlX8_QpI',
    appId: '1:469897133618:web:40bfa7c7a838cbb493862b',
    messagingSenderId: '469897133618',
    projectId: 'metal-collector',
    authDomain: 'metal-collector.firebaseapp.com',
    storageBucket: 'metal-collector.appspot.com',
    measurementId: 'G-GNF7Y57ZBX',
  );

  static const FirebaseOptions android = FirebaseOptions(
    apiKey: 'AIzaSyARmXxVWlxVUFP_VsPzeXige1L2M089ZBk',
    appId: '1:469897133618:android:a64ec587b7d17f9293862b',
    messagingSenderId: '469897133618',
    projectId: 'metal-collector',
    storageBucket: 'metal-collector.appspot.com',
  );

  static const FirebaseOptions ios = FirebaseOptions(
    apiKey: 'AIzaSyDoyvGzqssAHXYFJ3cEhHvGsoFfB3X42ac',
    appId: '1:469897133618:ios:c69ffe0976328b7993862b',
    messagingSenderId: '469897133618',
    projectId: 'metal-collector',
    storageBucket: 'metal-collector.appspot.com',
    iosClientId: '469897133618-1kvf2f3schucv5omdbaiinqqcg4q9rcg.apps.googleusercontent.com',
    iosBundleId: 'com.example.metalCollector',
  );

  static const FirebaseOptions macos = FirebaseOptions(
    apiKey: 'AIzaSyDoyvGzqssAHXYFJ3cEhHvGsoFfB3X42ac',
    appId: '1:469897133618:ios:dde48dcc83414dcb93862b',
    messagingSenderId: '469897133618',
    projectId: 'metal-collector',
    storageBucket: 'metal-collector.appspot.com',
    iosClientId: '469897133618-ubs79nvnbej1edri3bn2oc6smhqne7mm.apps.googleusercontent.com',
    iosBundleId: 'com.example.metalCollector.RunnerTests',
  );
}
