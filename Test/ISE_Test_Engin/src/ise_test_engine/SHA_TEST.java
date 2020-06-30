/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.security.NoSuchAlgorithmException;

/**
 *
 * @author M.alkahtani
 */
public class SHA_TEST {

    public static void main(String[] args) throws IOException {
        BufferedReader userInput = new BufferedReader (new InputStreamReader(System.in));

        System.out.println("Enter string:");
        String rawString = userInput.readLine();

        try {
            System.out.println("SHA1 hash of string: " + SHA.SHA(rawString));
        } catch (NoSuchAlgorithmException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        } catch (UnsupportedEncodingException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }
}