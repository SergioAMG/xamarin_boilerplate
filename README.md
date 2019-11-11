![](https://git.epam.com/sergio_martinez/xamarinboilerplate/raw/dev/epam_logotype.PNG)

**Xamarin Boilerplate**
=============
### Last updated: 07 November 2019 v 0.0.1

#### Authors, Log and Info:

Date  | Author  | Log
------------- | -------------| -------------
07/11/1029  | Sergio Martinez ([Sergio_Martinez@epam.com](mailto:Sergio_Martinez@epam.com "Sergio_Martinez@epam.com")) | File Creation

Version  | Author  | Action | Date
------------- | -------------| -------------| -------------
v 0.0.1| EPAM Systems | File Creation | 07/11/2019
v 0.0.2| EPAM Systems | File Modification | 11/11/2019

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

#### c) List of Features per Release:

##### Navigation Service (v 0.0.1)
Purpose: To provide a better navigation system based on viewmodels and registered pages. (Supports parameters and actions)
+ Features
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
	
##### Base View Model (v 0.0.1)
Purpose: To share common behaviors across multiple viewmodels.

+ Features
	+ Title
	+ Subtitle
	+ Navigation Service
	+ GoBackCommand()
	+ IsTitleCentered
	+ CallBackAction
	+ OnPropertyChanged

##### Command Extended (v 0.0.1)
Purpose: To prevent users to double-tap a button in a screen.
+ Features
	+ CommandManager
	+ CanExecute()
	+ ExecuteTaskCommand()
	+ ExecuteAction()
	+ ExecuteSelectedItemCommand()

##### Base Content Page (v 0.0.1)
Purpose: To extend content page adding support for extra properties, commands and events on Android.
 + Features
    + EnableHardwareBackButtonOverride
    + EnableLogicalBackButtonOverride
	+ HardwareBackButtonCommand
	+ LogicalBackButtonCommand
	+ HasNavigationBar

##### Loading Popup (v 0.0.1)
Purpose: To show the users a visual indication that the app is doing some work in the background.
+ Features
    * Customizable UI
    * ShowLoadingIndicator()
	* HideLoadingIndicator()

##### Application Common Styles (v 0.0.2)
Purpose: To enable the re-usage of common used visual styles across the whole application. This allows the application to have a consistent look and feel across pages.
+ Features
    + Custom Colors
    + Custom Styles

##### Identify Device (v 0.0.2)
Purpose: To allow the application to detect what kind of device the end user is using to access the application.
+ Features
    + Platform
	+ Manufacturer
	+ Version
	+ Idiom
	+ Device
	+ Orientation

##### Android Custom Styles (v 0.0.2)
Purpose: To customize some parts of the native behaviour while in Android
+ Features
    + Custom Status Bar Color
    + Custom Status Bar Icons Color

##### Device Enum (v 0.0.2)
Purpose: To provide a standardized way of device variations
+ Features
    + Device Info (based on Xamarin.Essentials)

##### Tint Effect (v 0.0.2)
Purpose: To provide a way to change the tint color of icons, buttons, and images in a standardized approach and from the shared code of the application.
+ Features
    + Tint Effect (Implemented in both platforms IOS & Android)

##### Custom Extended Android Button (v 0.0.2)
Purpose: To provide a way to change the text position within a button container. This helps to mimic IOS features in Andriod.
+ Features
    + HorizontalTextAlignment (This only applies to Android buttons)

##### Custom Extended Label (v 0.0.2)
Purpose: To provide a way to justify the text inside a given label.
+ Features
    + JustifyText (This only applies to IOS and Android 6+)

##### Device Manager (v 0.0.2)
Purpose: To provide a way to interact with the device in order to provide basic information about it. This will change with every single user. And the information stored inside this singleton object will be shared across pages in order to customize the look and feel of the application while responding to external events such as rotation.
+ Features
    + Platform
	+ Manufacturer
	+ Version
	+ Idiom
	+ Device
	+ Orientation
	+ IsAndroid()
	+ IsIOS()
	+ IsTablet()
	+ IsLandscape()

##### Wizzard (v 0.0.2)
Purpose: To provide a way to present the users basic information about how to use the application and how to get through basic troubleshooting. This will be loaded the first time user access the app, otherwise they will be redirected to the main landing page.
+ Features
    + StepOnePage (UI ready with support for multiple devices and multiple screens)
	+ StepOneViewModel
	+ StepTwoPage (UI ready with support for multiple devices and multiple screens)
	+ StepTwoViewModel
	+ StepThreePage (UI ready with support for multiple devices and multiple screens)
	+ StepThreeViewModel

#### d) Overall Status of App with latest Release:
**Date 11/11/2019**
**Relase v 0.0.2**
The application now contains a basic Wizzard sample with images and text in order to present initial information about how to use the app. Several customizations have been done in order to create a unified cross-platform experience. Still in process of importing all functionality created for the IFT demo prototype and create the configuration required for supporting IOS 11 device.