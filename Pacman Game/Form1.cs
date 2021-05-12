using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman_Game
{
    public partial class PacmanGame : Form
    {

        bool goUp, goDown, goLeft, goRight, isOver;

        int score, pacmanSpeed, redSpeed, orangeSpeed, pinkSpeed;

        public PacmanGame()
        {
            InitializeComponent();

            reset();
        }

        //Key event bindings
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                goUp = true;
                goDown = false;
                goLeft = false;
                goRight = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                goUp = false;
                goLeft = false;
                goRight = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                goUp = false;
                goDown = false;

            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                goUp = false;
                goDown = false;
                goLeft = false;
            }
            
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Up)
            //{
            //    goUp = false;
            //}
            //if (e.KeyCode == Keys.Down)
            //{
            //    goDown = false;
            //}
            //if (e.KeyCode == Keys.Left)
            //{
            //    goLeft = false;
            //}
            //if (e.KeyCode == Keys.Right)
            //{
            //    goRight = false;
            //}
            if (e.KeyCode == Keys.Enter && isOver)
            {
                reset();
            }
        }

        //Where the game runs
        private void mainGameTimer(object sender, EventArgs e)
        {

            //Update Score
            lbl_Score.Text = "Score: " + score;

            //Player Movement
            if (goLeft)
            {
                pacman.Image = Properties.Resources.left;
                pacman.Left -= pacmanSpeed;
            }
            if (goRight)
            {
                pacman.Image = Properties.Resources.right;
                pacman.Left += pacmanSpeed;
            }
            if (goUp)
            {
                pacman.Image = Properties.Resources.Up;
                pacman.Top -= pacmanSpeed;
            }
            if (goDown)
            {
                pacman.Image = Properties.Resources.down;
                pacman.Top += pacmanSpeed;
            }

            //Leaving Form bounds
            if (pacman.Left < -10)
            {
                pacman.Left = 752;
            }
            if (pacman.Left > 752)
            {
                pacman.Left = -10;
            }
            if (pacman.Top < -10)
            {
                pacman.Top = 610;
            }
            if (pacman.Top > 610)
            {
                pacman.Top = -10;
            }

            //Collision
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin" && x.Visible)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }
                    }
                    if ((string)x.Tag == "wall")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            if(goDown)
                            {
                                pacman.Top -= 8;
                            }
                            if(goUp)
                            {
                                pacman.Top += 8;
                            }
                            if (goLeft)
                            {
                                pacman.Left += 8;
                            }
                            if (goRight)
                            {
                                pacman.Left -= 8;
                            }
                        }
                    }
                    if ((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            //Game is over
                            gameOver("You Lost");
                        }
                    }
                }
            }


            //ghost movement
            ghostRed.Left += redSpeed;
            if (ghostRed.Left < -10 || ghostRed.Left > 700)
            {
                redSpeed = -redSpeed;
            }

            ghostOrange.Left += orangeSpeed;
            if (ghostOrange.Left < -10 || ghostOrange.Left > 700)
            {
                orangeSpeed = -orangeSpeed;
            }

            ghostPink.Top += pinkSpeed;
            if (ghostPink.Bounds.IntersectsWith(pictureBox3.Bounds) || ghostPink.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                pinkSpeed = -pinkSpeed;
            }


            if (score == 50)
            {
                //Game is Over
                gameOver("You Won!");
            }
        }

        private void gameOver(string player_mssg)
        {
            isOver = true;
            gameTimer.Stop();

            //lbl_Score.Text = "Score: " + score + Environment.NewLine + player_mssg;

            //Display Game Over Screen
            foreach (Control x in this.Controls)
            {
                if ((string)x.Tag == "Game Over")
                {
                    x.Visible = true;
                }
                else
                {
                    x.Visible = false;
                }

            }

            gameStatus.Text = player_mssg;
            finalScore.Text = "Score: " + score;
        }

        //reset to play again
        private void reset()
        {
            lbl_Score.Text = "Score: 0";
            score = 0;

            isOver = false;

            pacman.Left = 10;
            pacman.Top = 226;
            pacmanSpeed = 8;

            ghostRed.Left = 370;
            ghostRed.Top = 10;
            redSpeed = 5;

            ghostPink.Left = 379;
            ghostPink.Top = 235;
            pinkSpeed = 5;

            ghostOrange.Left = 370;
            ghostOrange.Top = 509;
            orangeSpeed = 5;

            //Makes all pictureBoxes visible
            //foreach (Control x in this.Controls)
            //{
            //    if (x is PictureBox)
            //    {
            //        x.Visible = true;
            //    }
            //}

            //Hides Game over Screen and makes everything else visible
            foreach (Control x in this.Controls)
            {
                if ((string)x.Tag == "Game Over")
                {
                    x.Visible = false;
                }
                else
                {
                    x.Visible = true;
                }

            }

            gameTimer.Start();
        }


    }
}
