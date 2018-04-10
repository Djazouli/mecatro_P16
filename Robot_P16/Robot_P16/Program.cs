using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

using Robot_P16.Actions;
using Robot_P16.Actions.ActionsIHM;
using Robot_P16.Robot.composants.IHM;
using Gadgeteer.Modules.GHIElectronics;

namespace Robot_P16
{
    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");


            //Parametrization parameters = new CustomParametrization(Robot.TypeRobot.TEST_ROBOT_1, Robot.ModeOperatoire.TEST1, Robot.CouleurEquipe.UNDEFINED);
            C_IHM IHM = new C_IHM();
            new Robot.Robot(IHM, IHM);

            Robot.GestionnaireAction.loadActions();

            IHM.startMethod();

            /*ActionBouton actionBouton1 = new ActionBouton(button);
            ActionBouton actionBouton2 = new ActionBouton(button2);
            ActionPipot actionPipot = new ActionPipot(6);
            Action[] tab0 = {actionBouton2, actionPipot};
            ActionEnParallele actionPara = new ActionEnParallele(tab0, "Test para");
            Action[] tab1 = { actionBouton1, actionPara };
            ActionEnSerie actionSerie = new ActionEnSerie(tab1, "Test série");

            new Thread(() => { actionSerie.Execute(); }).Start();*/
            
            
        }
    }
}
