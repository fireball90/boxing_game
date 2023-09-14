using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxingGame
{
    public partial class Form1 : Form
    {
        bool blockAttack = false;

        List<string> enemyAttacks = new List<string> { "left", "right", "block" };
        Random rnd = new Random();

        int enemySpeed = 5;
        int i = 0;
        bool enemyBlocked;

        int playerHealth = 100;
        int enemyHealth = 100; 
        public Form1()
        {
            InitializeComponent();
            playerLife.ForeColor = Color.Blue;
            enemyLife.ForeColor = Color.Red; //nem működik pls
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void enemyMoveEvent(object sender, EventArgs e)
        {
            enemyBoxer.Left += enemySpeed; 
            //életek
            if (playerHealth > 1)
            {
                playerLife.Value = Convert.ToInt32(playerHealth);
            }

            if (enemyHealth > 1)
            {
                enemyLife.Value = Convert.ToInt32(enemyHealth);
            }


            //ellenfél mozgás
            if (enemyBoxer.Left > 480)
            {
                enemySpeed = -5;
            }
            if (enemyBoxer.Left < 315)
            {
                enemySpeed = 5;
            }
            //életek
            if (enemyHealth < 1)
            {
                enemyTimer.Stop();
                enemyMove.Stop();
                MessageBox.Show("Nyertél! Kattinst egy új körért!");
                
                resetGame();
            }

            if (playerHealth < 1)
            {
                enemyTimer.Stop();
                enemyMove.Stop();
                MessageBox.Show("Kemény Józsi Nyert! Kattints egy új körért!");

                resetGame();
            }
        }

        private void enemyPunchEvent(object sender, EventArgs e)
        {
            i = rnd.Next(0, enemyAttacks.Count);

            switch (enemyAttacks[i].ToString())
            {
                //bal oldal
                case "left":
                    enemyBoxer.Image = Properties.Resources.enemy_punch1;

                    if (enemyBoxer.Bounds.IntersectsWith(boxer.Bounds) && !blockAttack)
                    {
                        playerHealth -= 20;
                    }
                    enemyBlocked = false; 
                    break;

                //jobb oldal
                case "right":
                    enemyBoxer.Image = Properties.Resources.enemy_punch2;

                    if (enemyBoxer.Bounds.IntersectsWith(boxer.Bounds) && !blockAttack)
                    {
                        playerHealth -= 20;
                    }
                    enemyBlocked = false;
                    break;

                //block
                case "block":

                    enemyBoxer.Image = Properties.Resources.enemy_block;

                    enemyBlocked = true;
                    break;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                boxer.Image = Properties.Resources.boxer_block;
                blockAttack = true;
            }

            if (e.KeyCode == Keys.Left)
            {
                boxer.Image = Properties.Resources.boxer_left_punch;

                if (enemyBoxer.Bounds.IntersectsWith(boxer.Bounds) && !enemyBlocked)
                {
                    enemyHealth -= 5;
                }
            }

            if (e.KeyCode == Keys.Right)
            {
                boxer.Image = Properties.Resources.boxer_right_punch;

                if (enemyBoxer.Bounds.IntersectsWith(boxer.Bounds) && !enemyBlocked)
                {
                    enemyHealth -= 5;
                }
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            boxer.Image = Properties.Resources.boxer_stand;
            blockAttack = false;
        }
        private void resetGame()
        {
            enemyTimer.Start();
            enemyMove.Start(); 

            enemyBoxer.Left = 357; 
            enemyBoxer.Top = 248; 

            enemyBoxer.Image = Properties.Resources.enemy_stand; 
            boxer.Image = Properties.Resources.boxer_stand; 

            playerHealth = 100; 
            enemyHealth = 100;
        }
    }
}
