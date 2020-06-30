/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine.core;

/**
 *
 * @author TOSHIBA
 */
public class Question {
    private int DB_ID;
    private String Text;
    private int Type;
    private float Wight;
  //  private int N_Answers;
    public Question(int db_id, String text, int type, float wight){

        this.DB_ID = db_id;
        this.Text = text;
        this.Type = type;
        this.Wight = wight;
       // this.N_Answers = n_answers;
    }

public int get_id(){
    return this.DB_ID;
}
public int get_type(){
    return this.Type;
}
public float get_wight(){
    return this.Wight;
}
public String fet_text(){
    return this.Text;
}
    @Override
public String toString(){
    return this.Text;
}
}
