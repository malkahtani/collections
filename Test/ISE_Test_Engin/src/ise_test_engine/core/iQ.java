/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine.core;

/**
 *
 * @author M.alkahtani
 */
public interface iQ {
	public static final int MAX_ANSWERS=10;
        
	public boolean isComplete();
	public boolean isMarked();
	public boolean isCorrect();

	public int getQuestionNumber();
	public void setQuestionNumber(int i);

	public int getType();
	public void setType(int type);

	public Object[] getCorrectAnswers();
	public Object[] getAllAnswers();

	public Object getExibit();
	public void setExibit();
	public void mark();

	public int getMinSelectable();
	public void setMinSelectable(int i);

	public String getXML();
	public String getText();
	public void setText(String s);

	public void addAnswer(iA a);
	public void setAnswer(int i,iA a);
	public void clearAnswers();
}
