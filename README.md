**Xamarin Boilerplate**
=============
### Last updated: 07 November 2019 v 0.0.1

#### Authors, Log and Info:

Date  | Author  | Log
------------- | -------------| -------------
07/11/1029  | Sergio Martinez ([Sergio_Martinez@epam.com](mailto:Sergio_Martinez@epam.com "Sergio_Martinez@epam.com")) | File Creation

Version  | Author  | Action
------------- | -------------| -------------
v 0.0.1| EPAM Systems | File Creation

Source Code  | Repository  | Branch
------------- | -------------| -------------
[git.epam.com](git.epam.com "git.epam.com") | [xamarinboilerplate](https://git.epam.com/sergio_martinez/xamarinboilerplate "xamarinboilerplate") | master

> "Everyday life is like programming, I guess. If you love something you can put beauty into it."

**Donald Knuth**

### General Boileplate Content & Description

#### b) About:
This boilerplate contains the basic structure for a custom navigation system that replaces the existing Xamarin navigation approach. 
This new method enables the developers to navigate between pages from the corresponding viewmodels.
At the same time, enables the passing of parameters and actions creating a more robust way of organizing the navigation paradigm in future apps.

#### b) List of External Libraries:
| Library  | Target  | Version |  Purpose |
| ------------ | ------------ | ------------ | ------------ |
|  AutoFac  | All Projects  | v 4.9.4  |  Injection of Containers |
| Net Standard Library  | All Projects  | v 2.0.3  | Provide .NET Framework support to development code |
| Rg Plugin Popup  | All Projects  | v 1.2.0.223  | Enable the use of custom Popups  |
| Xamarin Android Support Core Utils  | Android  | v 28.0.0.3  |  Support Library |
| Xamarin Essentials  |  All Projects |  v 1.3.1 | Provide Device Access Tools  |
| Xamarin Forms  | All Projects  |  v 4.3.0.947036 |  Provide support for Mobility Framework  |

#### c) List of Features in Release:

##### Navigation Service
	+ **Purpose:** To provide a better navigation system based on viewmodels and registered pages. (Supports parameters and actions)
	+ Configure (page key, page type)
	+ SetRootPage (page key, viewmodel)
	+ GoBackAsync()
	+ OpenDrawer()
	+ CloseDrawer()
	+ ShowLoadingIndicator()
	+ HideLoadingIndicator()
	+ OpenPopup (popup page key)
	+ ClosePopup()
	+ NavigateDetails (page key, parameter)
	+ NavigateModalAsync (page key)
	+ NavigateModalAsync (page key, parameter)
	+ NavigateModalAsync (page key, parameter, action)
	+ NavigateAsync (page key, parameter)
	+ NavigateAsync (page key, parameter, action)
	+ GetPage(page key, parameter, action)
	+ ViewModelByKey (page key)
	+ DisplayAlertController(values) (causes a crash in iOS 12+)
##### Base View Model
	+ **Purpose:** To share common behaviors across multiple viewmodels.
	+ Title
	+ Subtitle
	+ Navigation Service
	+ GoBackCommand()
	+ IsTitleCentered
	+ CallBackAction
	+ OnPropertyChanged
##### Command Extended
	+ **Purpose:** To prevent users to double-tap a button in a screen.
	+ CommandManager
	+ CanExecute()
	+ ExecuteTaskCommand()
	+ ExecuteAction()
	+ ExecuteSelectedItemCommand()
##### Base Content Page
	+ **Purpose:** To extend content page adding support for extra properties, commands and events on Android.
    + EnableHardwareBackButtonOverride
    + EnableLogicalBackButtonOverride
	+ HardwareBackButtonCommand
	+ LogicalBackButtonCommand
	+ HasNavigationBar
##### Loading Popup
	+ **Purpose:** To show the users a visual indication that the app is doing some work in the background.
    * Customizable UI
    * ShowLoadingIndicator()
	* HideLoadingIndicator()