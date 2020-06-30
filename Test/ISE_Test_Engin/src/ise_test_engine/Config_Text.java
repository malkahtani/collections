/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine;
import java.io.*;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.util.Scanner;
import javax.crypto.spec.SecretKeySpec;


/**
 *
 * @author TOSHIBA
 */
public class Config_Text {
     File Config = new File("config.txt");
     String DB;
     String DB_Pass;
     String DB_user;
     String Server;
     String Port;
     String admin;
     String pass;

    public Config_Text(String D, String D_Pass, String D_user, String S, String P, String Ad, String Pa){
        this.DB = D;
        this.DB_Pass = D_Pass;
        this.DB_user = D_user;
        this.Server = S;
        this.Port = P;
        this.admin = Ad;
        this.pass = Pa;


    }
    public Config_Text(){
    }
    
    public void write() throws IOException, InvalidKeyException, NoSuchAlgorithmException, InvalidKeySpecException  {
        byte key[] = "abcdEFGH".getBytes(); 
        SecretKeySpec secretKey = new SecretKeySpec(key,"DES");
    StringEncrypter desedeEncrypter = new StringEncrypter(secretKey, "DES");

    Writer out = new OutputStreamWriter(new FileOutputStream("config.txt"));
    try {
      out.write(desedeEncrypter.encrypt(this.admin));
      out.write(System.getProperty("line.separator"));
      out.write(desedeEncrypter.encrypt(this.pass));
      out.write(System.getProperty("line.separator"));
      out.write(desedeEncrypter.encrypt(this.Server));
      out.write(System.getProperty("line.separator"));
      out.write(desedeEncrypter.encrypt(this.Port));
      out.write(System.getProperty("line.separator"));
      out.write(desedeEncrypter.encrypt(this.DB));
      out.write(System.getProperty("line.separator"));
      out.write(desedeEncrypter.encrypt(this.DB_user));
      out.write(System.getProperty("line.separator"));
      out.write(desedeEncrypter.encrypt(this.DB_Pass));
      out.write(System.getProperty("line.separator"));

    }
    finally {
      out.close();
    }

    }
    public void read() throws IOException, InvalidKeyException, NoSuchAlgorithmException, InvalidKeySpecException {
    Scanner scanner = new Scanner(this.Config);
    
    byte key[] = "abcdEFGH".getBytes();

     SecretKeySpec secretKey = new SecretKeySpec(key,"DES");

    StringEncrypter desedeEncrypter = new StringEncrypter(secretKey, "DES");

    try {
      while (scanner.hasNextLine()){
        this.admin = desedeEncrypter.decrypt(scanner.nextLine());
        this.pass = desedeEncrypter.decrypt(scanner.nextLine());
        this.Server = desedeEncrypter.decrypt(scanner.nextLine());
        this.Port = desedeEncrypter.decrypt(scanner.nextLine());
        this.DB = desedeEncrypter.decrypt(scanner.nextLine());
        this.DB_user = desedeEncrypter.decrypt(scanner.nextLine());
        this.DB_Pass = desedeEncrypter.decrypt(scanner.nextLine());
      }
    }
    finally{
      scanner.close();
    }


    }


    }