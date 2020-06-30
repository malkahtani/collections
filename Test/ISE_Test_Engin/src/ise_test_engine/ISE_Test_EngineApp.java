/*
 * ISE_Test_EnginApp.java
 */

package ise_test_engine;

import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import org.jdesktop.application.Application;
import org.jdesktop.application.SingleFrameApplication;
import java.io.*;


/**
 * The main class of the application.
 */
public class ISE_Test_EngineApp extends SingleFrameApplication {

    /**
     * At startup create and show the main frame of the application.
     */
    @Override protected void startup() {


        show(new ISE_Test_EngineView(this));
    }

    /**
     * This method is to initialize the specified window by injecting resources.
     * Windows shown in our application come fully initialized from the GUI
     * builder, so this additional configuration is not needed.
     */
    @Override protected void configureWindow(java.awt.Window root) {
    }

    /**
     * A convenient static getter for the application instance.
     * @return the instance of ISE_Test_EnginApp
     */
    public static ISE_Test_EngineApp getApplication() {
        return Application.getInstance(ISE_Test_EngineApp.class);
    }

    /**
     * Main method launching the application.
     */
    public static void main(String[] args) throws InvalidKeyException, NoSuchAlgorithmException, InvalidKeySpecException, ClassNotFoundException {
        File Config = new File("config.txt");
//StringBuffer contents = new StringBuffer();
//BufferedReader reader = null;

        if (!Config.exists()) {
      java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ISE_Test_EngineConfigure().setVisible(true);
            }
        });
        System.out.print("No Confige");
    }else{
            java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new login().setVisible(true);
            }
        });

        }
       
        
    }
    }
