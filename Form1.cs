using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Module5._5Practical2
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        public void clearTB()
        {
            firstNameTB.Text = "";
            lastNameTB.Text = "";
            majorCB.Text = "";
        }

        public void findRow(String firstName, String lastName)
        {
            var context = new FabrikamEntities();

            var query =
                from c in context.Students
                where c.FirstName == firstNameTB.Text && c.LastName == lastNameTB.Text
                select c;
            if(!String.IsNullOrEmpty(firstNameTB.Text) && !String.IsNullOrEmpty(lastNameTB.Text))
            {
                foreach (var Student in query) 
                {
                    majorCB.Text = Student.Major;
                }
            } else
            {
                MessageBox.Show("You must specify the exact first and last element of the entity you want to find.");
            }
        }

        public void updateMajor(String firstName, String lastName)
        {
            var context = new FabrikamEntities();

            var query =
                from c in context.Students
                where c.FirstName == firstNameTB.Text && c.LastName == lastNameTB.Text
                select c;
            if (!String.IsNullOrEmpty(firstNameTB.Text) && !String.IsNullOrEmpty(lastNameTB.Text))
            {
                foreach (var Student in query)
                {
                    Student.Major = majorCB.Text;
                }
                context.SaveChanges();
            }
            else
            {
                MessageBox.Show("You must specify the exact first and last element of the entity you want to update.");
            }
        }

        public void deleteRow(String firstName, String lastName)
        {
            var context = new FabrikamEntities();

            var query =
                from c in context.Students
                where c.FirstName == firstNameTB.Text && c.LastName == lastNameTB.Text
                select c;

            if(!String.IsNullOrEmpty(firstNameTB.Text) && !String.IsNullOrEmpty(lastNameTB.Text))
            {
                foreach(var Student in query)
                {
                    context.Students.Remove(Student);
                }
                context.SaveChanges();
            } else
            {
                MessageBox.Show("You must specify the exact FirstName and LastName of the entity you'd like to delete.");
            }
        }

        public void addRow(String firstName, String lastName)
        {
            var context = new FabrikamEntities();
            if(!String.IsNullOrEmpty(firstNameTB.Text) && !String.IsNullOrEmpty(lastNameTB.Text))
            {
                Student newStud = new Student()
                {
                    FirstName = firstNameTB.Text,
                    LastName = lastNameTB.Text,
                    Major = majorCB.Text
                };
                context.Students.Add(newStud);
                context.SaveChanges();
            } else
            {
                MessageBox.Show("You must enter a first and last name.");
            }
        }

        public bool isFound(String firstName, String lastName)
        {
            var context = new FabrikamEntities();
            var query =
                from c in context.Students
                where c.FirstName == firstNameTB.Text && c.LastName == lastNameTB.Text
                select c;

            return query.Any();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isFound(firstNameTB.Text, lastNameTB.Text))
            {
                findRow(firstNameTB.Text, lastNameTB.Text);
            } else
            {
                MessageBox.Show("Entity not found.");
            }
        }

        private void firstNameTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if(isFound(firstNameTB.Text, lastNameTB.Text))
            {
                updateMajor(firstNameTB.Text, lastNameTB.Text);
                clearTB();
                MessageBox.Show("Update successful.");
            } else
            {
                MessageBox.Show("Entity not found.");
            }
            
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if(isFound(firstNameTB.Text, lastNameTB.Text))
            {
                MessageBox.Show("Entity already exists in database.");
            } else
            {
                addRow(firstNameTB.Text, lastNameTB.Text);
                clearTB();
                MessageBox.Show("Add successful.");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if(isFound(firstNameTB.Text, lastNameTB.Text))
            {
                deleteRow(firstNameTB.Text, lastNameTB.Text);
                clearTB();
                MessageBox.Show("Delete successful.");
            } else
            {
                MessageBox.Show("Entity not found");
            }
        }
    }
}
