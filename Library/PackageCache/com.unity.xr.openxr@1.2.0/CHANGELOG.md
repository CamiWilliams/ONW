# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.2.0] - 2021-05-06

* Enabled Android build target for Oculus Quest via the `Oculus Quest Support` feature.
* Removed `experimental` text from OpenXR plugin help icon.
* Removed `Linear` color space restriction for all build targets and graphics apis except OpenGLES.
* Fixed bug with haptics that caused `XRControllerWithRumble.SendImpulse` to not work with `OpenXR`.
* Fixed bug that could cause some interaction profile device layouts to not be registered on startup.
* OpenXR Settings UI reworked to make managing features and interaction profiles easier.
* Added menu item to open Project Validation window (`Window > XR > OpenXR > Project Validation`).
* Project validation window now supports manual fixes for an issue.
* Project validation window now supports optional help links for an issue.
* Added `OpenXRFeature.OnEnableChanged` method to give features a chance to handle their enabled state changing.
* Added `IPackageSettings.GetFeatures` method that returns all features of a given type from all build targets.

## [1.1.1] - 2021-04-06
* Oculus controller profile now exposes both grip and aim poses.
* Fixed issue where OpenXR layouts were not visible in the InputSystem bindings dialog.
* `OpenXRSettings.renderMode` and `OpenXrSettings.depthSubmissionMode` can now be changed via script outside of play mode.
* Fix for managed stripping levels of Medium and High
* Fixed bugs in `XR_KHR_VULKAN_ENABLE2` extension support
* Added support for `XR_VARJO_QUAD_VIEWS` extension 
* Added `XR_COMPOSITION_LAYER_UNPREMULTIPLIED_ALPHA_BIT` and `XR_COMPOSITION_LAYER_BLEND_TEXTURE_SOURCE_ALPHA_BIT` bits to the composition layer flags 
* Added `XrSecondaryViewConfigurationSwapchainCreateInfoMSFT` to to `XrSwapchainCreateInfo` when using a secondary view
* MockRuntime First Person Observer View support
* MockRuntime input support
* MockRuntime vulkan_enable2 support
* MockRuntime d3d11_enable support

## [1.0.2] - 2021-02-04
* Resolve further release verification issues.

## [1.0.1] - 2021-02-03
* Resolve release verification issues.

## [1.0.0] - 2021-01-27
* Runtime Debugger to allow for the inspection of OpenXR calls that occur while OpenXR is actively running.
* XR Plug-In Management dependency update to 4.0.1
* Input System dependency updated to 1.0.2

## [0.1.2-preview.3] - 2021-01-19

* Implemented `XR_KHR_loader_init` and `XR_KHR_loader_init_android`.
* Updated dependency of `com.unity.xr.management` from `4.0.0-pre.2` to `4.0.0-pre.3`.
* Added support for `XR_KHR_vulkan_enable2` alongside `XR_KHR_vulkan_enable`.

## [0.1.2-preview.2] - 2021-01-05

* Fix publishing pipeline.

## [0.1.2-preview.1] - 2020-12-18

* Fixed package errors when Analytics package is absent (case 1300418).
* Fixed tracking origin issue which was causing wrong camera height (case 1300457).
* Fixed issue where large portions of the world were incorrectly culled at certain camera orientations.
* Fixed potential error message when clicking `Fix All` in OpenXR Project Validation window.
* Fixed an issue with sample importing.
* Minor documentation getting started tweaks.
* Minor diagnostic logging tweaks.

## [0.1.1-preview.1] - 2020-12-16

### This is the first release of *OpenXR Plugin \<com.unity.xr.openxr\>*.
