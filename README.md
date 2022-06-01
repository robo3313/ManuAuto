# Manu_Auto

## Langue
Le code est complètement en anglais.
Le site est complètement en anglais.

## Gitflow
Le gitflow utilisé sera le trunk-based development : https://www.atlassian.com/continuous-delivery/continuous-integration/trunk-based-development

Ce qu'il faut retenir :
Une branche représente une feature, plutôt simple (par exemple l'ajout d'utilisateurs, pas toute la gestion des utilisateurs).
Merge une fois par jour la branche dans le main.
Une fois que la branche est merge, on la supprime.

## Nommage du code
Les variables au sein d'une méthode ou fonction seront en camelCase.
Les attributs d'une classe, les noms de fonction, méthodes et classe sont en PascalCase.
Les index de tableau ou de JSON seront en snake_case.

## Initialiser le projet

Le projet est en code first, il est donc nécessaire de mettre à jour votre fichier "appsettings.json" avec la bonne "ConnectionStrings" pour se connecteur au serveur en local.
La Database fournie en paramètre ne doit pas déjà exister.