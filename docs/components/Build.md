# Overview
Editor build system with an editor menu, commandline builder and pre/post build events.

# Usage
1. Pre Build:  
1.1. Create a class with a static method that contains your pre build logic.  
1.2. Decorate the method with a `BuildEventAttribute` attribute and pass in `EventType.Pre`.

2. Post Build:  
2.1. Create a class with a static method that contains your post build logic.  
2.2. Decorate the method with a `BuildEventAttribute` attribute and pass in `EventType.Post`.

3. Build using the menu: `Tools/GameWork/Build/Active Target`.

**Note:**  
By specifying an `order` in the `BuildEventAttribute` you can control the execution order of the decorated method.  
By specifying one or more `BuildTarget`s in the `BuildEventAttribute` you can control which Build Targets the decorated method will be executed for.  

See the Tests project for example usage.