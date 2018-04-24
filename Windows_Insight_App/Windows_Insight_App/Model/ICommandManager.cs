using System;
using System.Windows.Input;

namespace Windows_Insight_App.Model
{
    /// <summary>
    /// Handles assigning commands to the controls on a window.
    /// </summary>
    static class ICommandManager
    {
        /// <summary>
        /// Determine what command is being looked for an return it.
        /// </summary>
        /// <param name="command">What command is being looked for.</param>
        /// <returns>The command to act on.</returns>
        public static ICommand SetupICommand(string command)
        {
            switch (command)
            {
                case "button":
                    return SetupButton();
                case "checkbox":
                    return SetupCheckbox();
                case "dropdown":
                    return SetupDropdown();
                case "radiobutton":
                    return SetupRadiobutton();
                default:
                    throw new NotImplementedException("Unrecognized ICommand.");

            }
        }

        /// <summary>
        /// Determine and deliver whatever the given button needs to do.
        /// </summary>
        /// <returns>The command to execute.</returns>
        private static ICommand SetupButton()
        {
            Command cmd = new Command(formBtn =>
            {
                System.Windows.Controls.Button btn = (System.Windows.Controls.Button)formBtn;
                try
                {
                    switch (btn.Content.ToString())
                    {
                        case "Update W/H":
                            try
                            {
                                int h = Convert.ToInt16(ViewModel.heightStatic);
                                int w = Convert.ToInt16(ViewModel.widthStatic);
                                int[] xy = ScreenshotManager.GetProcessXY();
                                ProcessManager.MoveWindow(ProcessManager.runningProc.MainWindowHandle, xy[0], xy[1], w, h, true);
                            }
                            catch ( Exception e )
                            {
                                ErrorManager.ManageError(ErrorManager.PossibleErrors.No_Process_Found);
                                // Put the below into a different error message?
                                //System.Windows.Message Box.Show("Unable to get the window to set the size.");
                            }
                            // Telemetry.log(TelemetryKey.set_hw_btn, "Clicked");
                            break;
                        case "Update":
                            // TODO : Consider putting this in ProcessManager?
                            if (!(ViewModel.manualNameStatic != string.Empty && ViewModel.manualNameStatic != "" && ViewModel.manualTitleStatic != string.Empty && ViewModel.manualTitleStatic != ""))
                            {
                                ErrorManager.ManageError(ErrorManager.PossibleErrors.No_NameTitle_Entered);
                                //System.Windows.Message Box.Show("Please enter a name and title for the process you want to connect to.");
                                return;
                            }
                            //ProcessManager.GetProcessByNameTitle(ViewModel.manualNameStatic, ViewModel.manualTitleStatic);
                            ProcessManager.StartProcessManager(ViewModel.manualNameStatic, ViewModel.manualTitleStatic);
                            // Telemetry.log(TelemetryKey.manual_btn, "Clicked");
                            break;
                        case "Screenshot":
                            ScreenshotManager.SaveShot();
                            // Telemetry.log(TelemetryKey.screenshot_btn, "Clicked");
                            break;
                        default:
                            // TODO : Making the speed buttons structs would help greatly with this checking.
                            // Check if the button is a quick-action button
                            if (btn.Name == "OneBtn" || btn.Name == "TwoBtn" || btn.Name == "ThreeBtn" || btn.Name == "FourBtn")
                            {
                                int btnNum = (btn.Name.Contains("One")) ? 1 : (
                                    (btn.Name.Contains("Two")) ? 2 : (
                                    (btn.Name.Contains("Three")) ? 3 : 4));
                                string name = (btnNum == 1) ? Properties.Settings.Default.oneProcName : (
                                    (btnNum == 2) ? Properties.Settings.Default.twoProcName : (
                                    (btnNum == 3) ? Properties.Settings.Default.threeProcName :
                                    Properties.Settings.Default.fourProcName));
                                string title = (btnNum == 1) ? Properties.Settings.Default.oneProcTitle : (
                                    (btnNum == 2) ? Properties.Settings.Default.twoProcTitle : (
                                    (btnNum == 3) ? Properties.Settings.Default.threeProcTitle :
                                    Properties.Settings.Default.fourProcTitle));

                                ProcessManager.StartProcessManager(name, title);

                                ViewModel.manualNameStatic = name;
                                ViewModel.manualTitleStatic = title;
                                // Telemetry.log(TelemetryKey.quick_btn, btnNum.ToString());
                            }
                            else
                            {
                                throw new NotImplementedException("Unrecognized Button");
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2146233080) // Index is outside of the array, meaning a too-high of number was entered.
                    { }
                    // TODO : Add better error catching here.
                    Console.Write("// TODO : FIX ME {0}", ex);
                }

            });

            return cmd;
        }

        /// <summary>
        /// Determine and deliver whatever the given checkbox needs to do.
        /// </summary>
        /// <returns>The command to execute.</returns>
        private static ICommand SetupCheckbox()
        {
            Command cmd = new Command(checkBox =>
            {
                System.Windows.Controls.CheckBox cb = (System.Windows.Controls.CheckBox)checkBox;
                bool isChecked = cb.IsChecked.Value;
                try
                {
                    switch (cb.Name)
                    {
                        case "WHcb":
                            if (!isChecked)
                            {
                                ViewModel.whCBCheckedStatic = false;
                                TimerManager.WHTimer.Stop();
                                TimerManager.PerformanceTimer.Stop();
                                //ViewModel.whContentStatic = "";
                                // Telemetry.log(TelemetryKey.hw_timer, "1");
                            }
                            else
                            {
                                ViewModel.whCBCheckedStatic = true;
                                TimerManager.WHTimer.Start();
                                TimerManager.PerformanceTimer.Start();
                                // Telemetry.log(TelemetryKey.hw_timer, "0");
                            }
                            break;
                        default:
                            throw new NotImplementedException("Unrecognized CheckBox");
                    }
                }
                catch (Exception ex)
                {
                    // TODO : Add better error handling here.
                    Console.Write("// TODO : FIX ME {0}", ex);
                    ViewModel.whCBCheckedStatic = false;
                    ViewModel.whContentStatic = "";
                }
            });

            return cmd;
        }

        /// <summary>
        /// Determine and deliver whatever the given dropdown needs to do.
        /// </summary>
        /// <returns>The command to execute.</returns>
        private static ICommand SetupDropdown()
        {
            return null;
        }

        /// <summary>
        /// Determine and deliver whatever the given radio-button needs to do.
        /// </summary>
        /// <returns>The command to execute.</returns>
        private static ICommand SetupRadiobutton()
        {
            return null;
        }
    }
}
