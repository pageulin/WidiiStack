# WidiiStack

Entraînement sur la stack de Widii

## Niveau 1

- Créer une API REST
- La documenter à l'aide de Swagger (SwashBuckle) et exposer une route pour la doc
- Ajouter une couche de persistance avec MongoDB
- Ajouter une validation du modèle (levée d'exception par middleware)

## Niveau 2
- Installer RabbitMQ (https://www.rabbitmq.com/install-windows.html)
- Lever un évènement depuis le projet d'API (https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html) (https://www.rabbitmq.com/devtools.html)
- Créer une deuxième API abonnée à l'évènement levé par la première

## Niveau 3
- Créer une application indentity Server 4 à partir de sample sur github
- Configurer la première API et un client lui correspondant
- Protéger la première API en ajoutant une validation par token oauth 2
- Configurer Swagger pour supporter l'authentification oauth 2

## Niveau 4
- Installer Docker
- Dockeriser les applications précédentes
- Déployer et tester les applications en local
