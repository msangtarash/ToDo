# ToDo
To do app developed with C#-Xamarin forms-Prism-Entity framework core

Structure:

  [To do project](src/ToDo) >> .NET standard library contains shared code across all platforms.

  [ToDoDbContext.cs](src/ToDo/DataAccess/ToDoDbContext.cs) >> Ef core db context class.
  
  [Models](src/ToDo/Model) >> ToDoGroup which has a collection of ToDoItem(s)
  
  [View models](src/ToDo/ViewModels) >> View models based on MVVM and Prism
  
  [Views](src/ToDo/Views) >> Xaml based views
  
  [Android project](src/ToDo.Droid) >> Android project
  
  [Android renderers](src/ToDo.Droid/Renderes) >> Android specific codes for UI
  
  [iOS project](src/ToDo.iOS) >> iOS project
