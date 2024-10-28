# MR Projection Tools

A collection of tools for projecting a VR environment onto a physical model or displaying it on a screen.

## Installation

To add this package to your Unity project, follow these steps:

1. Copy the web URL of this repository. You can find this URL under the "Code" drop-down above, in the HTTPS tab. Alternatively, you can copy it directly here:
   https://github.com/boathungry/mr-projection-tools.git
2. Navigate to Unity and save your project if you have not done so already, as you may need to restart the editor as part of the installation process.
3. Open Unity's Package Manager. Open the "+" drop-down menu and select "Add package from git URL..."
4. Paste the URL you copied earlier into the text box that appears and click the "Add" button.
5. Unity should now install the package and its dependencies. You may be prompted to enable the native input backends for Unity's new input system. If so, select "Yes" and allow Unity to restart your editor to finish the installation.

## Setup

This package has some pre-made assets to help set up Envalys's MR projection workflow. They require some set-up to function, detailed down below.

#### XR Interaction Toolkit configuration and starter assets

Some pre-made assets rely on starter assets from the XR Interaction Toolkit. You should already have the XR Interaction Toolkit installed, since this package lists it as a dependency. To set up these assets, follow these steps:

1. Open the Package Manager window in Unity and navigate to the XR Interaction Toolkit page.
2. Navigate to the "Samples" tab and import the Starter Assets by clicking on the "Import" button in the "Starter Assets" section.
3. In the XR Interaction Toolkit settings (Edit > Project Settings > XR Plug-in Management > XR Interaction Toolkit), expand the "Interaction Layers" section, and set User Layer 31 to "Teleport".
4. Go back to the Package Manager, navigate to the "Samples" tab of the Mixed Reality Projection Tools page, and import the assets labeled "Prefabs".
5. You should now have all necessary scripts and assets installed.
