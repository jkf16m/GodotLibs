# About shared and components.

Here there are reused code across projects I've made.

The components are simple Nodes, type base Node, used to implement a certain functionality to
a specific parent node.

They can be thought of as small functions that provide a good abstraction between too specific logic and
too broad logic.

There is also the lib folder, these classes and interfaces are not always coupled with
godot, they "could" be used in any kind of project, but that's not what they're meant for.