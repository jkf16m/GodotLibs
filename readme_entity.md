# About entities

These are derived from more concrete nodes, like `KinematicBody2D` or `Area2D`. They are meant to be used in a game, and they are organized by type.

Entities are composed of components, which are reusable pieces of logic.

Entities do not have to implement any interface, they simply have components or a specific logic.


Unlike components, entities are organized by feature, not by type.
Meaning that they have a more abstract name, like `Player` or `Enemy`.

Also entities, are meant to be independent from the game logic, and they can be used in any game.