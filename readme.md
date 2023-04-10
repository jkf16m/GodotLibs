# About shared, entities and components.


In godot, we have scenes, but for code reusability i decided to create this small repository of reusable code.

There are 3 main folders:

- components
- entities
- lib

When deciding whether to create an entity, or a component, you have to start from the specific
folder of the project, `features`.

When building a feature, I use the already created components to give my feature the required behaviour.
When I finish building my feature, then if I extract enough logic from it, I can create a new entity.
Now, this entity can be extracted and put into the `entities` folder, so it can be reused in later projects or
features.


- components are building blocks
- entities are the result of combining components