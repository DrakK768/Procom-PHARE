# PROjet COMplex - PHARE - Application de Réalité Augmentée pour la Découverte du Phare du Minou

## Fonctionnalités Principales

L'application PHARE offre les fonctionnalités suivantes :

* **Visualisation de Bâtiments Disparus en RA :** Affichage en temps réel de modèles 3D de deux bâtiments historiques (la maison des gardiens et l'hôtel-rôtisserie) sur le site du Phare du Minou, superposés à la vue de la caméra.
* **Détection d'Images Cibles :** Utilisation de l'image tracking (via ARCore pour Android et ARKit pour iOS) pour ancrer les modèles 3D à des emplacements spécifiques, marqués par des affiches placées sur le site.
* **Navigation Intuitive :** Une interface utilisateur simple permet aux utilisateurs de démarrer l'expérience en pointant leur appareil vers les affiches.
* **Exploration Intérieure et Extérieure :** Les utilisateurs peuvent se déplacer physiquement autour des modèles 3D pour explorer leur extérieur et, dans le cas de la maison des gardiens, l'intérieur.
* **Positionnement Manuel des Modèles :** Une option est disponible dans un menu pour ajuster manuellement la position du modèle 3D :
    * **Pinch:** Modification de la distance entre l'utilisateur et le modèle.
    * **Swipe:** Déplacement latéral ou vertical du modèle.
    * **Rotate** Rotation du modèle sur l'axe vertical.
* **Changement d'Époque (Fonctionnalité Future) :** Un menu déroulant est prévu pour permettre de visualiser les bâtiments à différentes époques, une fois que des modèles correspondant à ces époques seront ajoutés.
* **Capture d'Écran :** Possibilité de prendre une capture d'écran de la vue de l'application, incluant le modèle 3D mais sans l'interface utilisateur.
* **Informations et Tutoriel :** Des menus intégrés fournissent un guide d'utilisation et des informations sur l'application et son contexte.

## Technologies Utilisées

* **Moteur de Jeu :** Unity
* **Frameworks de Réalité Augmentée :**
    * ARCore (Android)
    * ARKit (iOS)
* **Logiciel de Modélisation 3D :** Blender
* **Bibliothèques d'Assets 3D :** Asset Store d'Unity, Poly Haven (pour les textures)

## Configuration et Installation

1.  **Téléchargement de l'Application :** L'application sera disponible sur les plateformes Android (Google Play Store) et iOS (App Store).
2.  **Installation des Panneaux :** Pour une expérience optimale, les utilisateurs devront se trouver sur le site du Phare du Minou et localiser les panneaux portant les affiches suivantes :
    * [Lien vers les affiches](https://www.canva.com/design/DAGe4W8vo68/lpLA7YCG3oI7WU7NtBapUg/edit?utm_content=DAGe4W8vo68&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton)
    * [Lien vers l'affiche du forum procom](https://www.canva.com/design/DAGg3cCzR2k/TegiRnCfnJMhhXS3q2XCbQ/edit?utm_content=DAGg3cCzR2k&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton)

## Architecture et Fonctionnement

### Système de Placement des Bâtiments

1.  **Détection des Images Cibles :** L'application utilise un `AR Tracked Image Manager` pour détecter les images de référence stockées dans la `Reference Image Library`.
2.  **Gestion de la Détection :** Un script `ImageTracker` est abonné à l'événement de détection d'images du `AR Tracked Image Manager`.
3.  **Placement et Orientation :** Lorsqu'une image est détectée, le script `ImageTracker` instancie et positionne le modèle 3D correspondant en fonction du nom interne de l'image détectée.
4.  **Cas Spécifiques :** Actuellement, le placement pour les noms d'images "poster", "poster2" et "poster3" est géré individuellement, permettant de définir des positions et rotations spécifiques dans l'éditeur Unity. Un placement par défaut est appliqué pour les autres images détectées, supposant une orientation verticale et une position standard.
5.  **Tracking Continu :** La position du modèle est mise à jour en temps réel par l'API d'ARCore/ARKit, en utilisant les capteurs de l'appareil. Le modèle est recalibré lorsque l'image cible est à nouveau détectée.

### Améliorations Potentielles du Système de Placement

* **Utilisation d'un PropertyDrawer :** Il est recommandé de remplacer le "hard-coding" des cas spécifiques par un `PropertyDrawer` pour sérialiser une liste d'objets contenant la référence de l'image et ses paramètres de placement (position, rotation). Cela permettrait d'ajouter et de gérer un nombre quelconque de cas sans modifier le code.
* **Abstraction de la Gestion des Modèles :** Le `AR Tracked Image Manager` ne devrait pas être directement responsable de la gestion des modèles. Le script `ImageTracker` gère actuellement cette logique.

### Positionnement Manuel

Le positionnement manuel est géré par le script `PositionManager`, qui utilise l'Input Action `TouchControls`. Les interactions suivantes sont disponibles :

* **Déplacement (Move) :** Glisser un doigt sur l'écran déplace le modèle sur une sphère centrée sur l'utilisateur.
* **Zoom (Pinch) :** Pincer l'écran avec deux doigts translate le modèle sur l'axe de vue, simulant un zoom sans modifier l'échelle réelle du bâtiment.
* **Rotation (Rotate) :** Tourner deux doigts en cercle fait pivoter le modèle autour de l'axe vertical.

### Interface Utilisateur (UI)

L'interface utilisateur est composée de plusieurs éléments :

* **Vue Principale :** Contient un menu de côté (hamburger) et un panneau inférieur avec des boutons.
* **Bottom Panel :**
    * **Bouton Capture d'Écran :** Désactive l'UI, prend une capture d'écran et la sauvegarde dans l'album photo du téléphone. L'album de sauvegarde peut être configuré dans le script `TakeScreenshot`.
    * **Bouton/Menu Changement d'Époque :** Un appui court permet de passer à l'époque suivante. Un appui long ouvre un menu déroulant pour choisir une époque spécifique. Un système d'Event est utilisé pour notifier les changements d'époque via le script `YearManager`.
* **Menu de Côté :** Accessible via le bouton hamburger, il donne accès aux menus suivants :
    * **Menu Guide :** Explique les éléments de l'interface à l'aide de textes et de flèches. Se ferme en touchant l'écran.
    * **Menu Démarrer :** Affiché au lancement de l'application, il fournit des informations générales et un bouton pour accéder à l'interface principale. Il est possible d'y revenir via le menu de côté.
* **Gestion de l'Orientation :** La mise en page de l'UI s'adapte automatiquement aux changements d'orientation de l'appareil, à l'exception du texte du menu Tutoriel, qui est ajusté par un script en fonction de l'orientation (positions prédéfinies dans l'inspecteur Unity).

### Modélisation des Bâtiments

* **Logiciel :** Blender a été utilisé pour la modélisation 3D en raison de son caractère open-source, de sa popularité et de sa maîtrise dans le cadre du projet.
* **Fichiers Blender :** Deux fichiers sont disponibles sur le dépôt GitHub : un pour la maison des gardiens (et son annexe) et un pour l'hôtel.
* **Structure et Textures :** La structure globale des bâtiments a été créée à partir de formes primitives (principalement des cubes). Les textures proviennent du site Poly Haven.
* **Détails et Aménagement Intérieur :** Les détails plus fins et l'aménagement intérieur ont été réalisés directement dans Unity, en utilisant des assets de l'Asset Store (y compris des assets payants acquis dans le cadre du projet, situés dans `/assets/libs`). Tous les assets utilisés sont libres de droits.
* **Fidélité Historique et Choix de Conception :**
    * La modélisation tente de se rapprocher au maximum des archives disponibles.
    * La verrière de l'hôtel n'a pas été modélisée en raison du manque d'archives.
    * Les deux bâtiments de la maison des gardiens ont été modélisés, même si historiquement ils n'ont pas été construits simultanément.
    * L'aménagement intérieur de la maison respecte les plans d'archives avec une interprétation, tandis que l'intérieur de l'hôtel n'est pas représentatif par manque de documentation et de temps.
* **Décorations d'Agrément :** Des éléments de décoration extérieure (plantes, bancs) ont été ajoutés pour rendre l'environnement plus vivant, suite aux retours positifs des testeurs.
* **Ressources Disponibles :** Les fichiers Blender et les textures utilisées sont disponibles sur le dépôt GitHub. Il est recommandé aux futurs développeurs de se baser sur la modélisation de l'hôtel comme exemple, car elle suit mieux les conventions établies.

## Dépôt GitHub

L'ensemble du code source, des fichiers Blender et des ressources du projet est disponible sur GitHub : [Lien vers le dépôt GitHub](https://github.com/DrakK768/Procom-PHARE)

## Conclusion et Perspectives

L'application PHARE, dans son état actuel, est fonctionnelle et permet de visualiser les bâtiments disparus sur le site du Phare du Minou grâce à la détection des panneaux. Bien que des ajustements de précision soient nécessaires, l'application offre une expérience utilisateur cohérente sur différents appareils.

Les améliorations futures pourraient se concentrer sur :

* **Amélioration de la Précision du Placement :** Investiguer des techniques de tracking plus avancées ou affiner les paramètres de détection.
* **Implémentation du Changement d'Époque :** Ajouter des modèles 3D correspondant aux différentes époques et finaliser la logique de changement d'époque.
