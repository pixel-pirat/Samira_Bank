Imports System.IO ' Required for image processing
Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient

Public Class UC_AddCustomer



    Dim conn As New MySqlConnection("server=localhost;user id=root;password=password1234567890//;database=banking_system;")
    Dim imgPath As String ' Store selected image path

    ' Function to Generate Unique ID
    Private Function GenerateUniqueID(length As Integer) As String
        Dim rnd As New Random()
        Dim result As String = ""
        For i As Integer = 1 To length
            result &= rnd.Next(0, 9).ToString()
        Next
        Return result
    End Function

    ' Function to Convert Image to Byte Array (for Database Storage)
    Private Function ConvertImageToBytes(ByVal img As Image) As Byte()
        Dim ms As New MemoryStream()
        img.Save(ms, Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    ' Browse and Select Profile Picture
    Private Sub btnUploadImage_Click(sender As Object, e As EventArgs) Handles btnUploadImage.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            imgPath = openFileDialog.FileName
            pbProfilePicture.Image = Image.FromFile(imgPath)
        End If
    End Sub

    ' Create Customer and Account
    Private Sub btnCreateCustomer_Click(sender As Object, e As EventArgs) Handles btnCreateCustomer.Click
        ' Validate Fields
        If txtFirstName.Text = "" Or txtLastName.Text = "" Or txtPhone.Text = "" Or txtAddress.Text = "" Or cmbMembershipType.SelectedItem Is Nothing Or txtBalance.Text = "" Or pbProfilePicture.Image Is Nothing Then
            MessageBox.Show("Please fill in all fields and upload a profile picture.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim transaction As MySqlTransaction = Nothing

        Try
            conn.Open()
            transaction = conn.BeginTransaction()

            ' Generate Unique IDs
            Dim customerId As String = GenerateUniqueID(10)
            Dim accountNumber As String = GenerateUniqueID(12)

            ' Convert Image to Byte Array
            Dim imgBytes As Byte() = ConvertImageToBytes(pbProfilePicture.Image)

            ' Insert Customer
            Dim customerQuery As String = "INSERT INTO customers (customer_id, first_name, last_name, date_of_birth, phone, address, membership_type, status, profile_picture) 
                                           VALUES (@customer_id, @first_name, @last_name, @dob, @phone, @address, @membership_type, 'Active', @profile_picture);"
            Using cmd As New MySqlCommand(customerQuery, conn, transaction)
                cmd.Parameters.AddWithValue("@customer_id", customerId)
                cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text)
                cmd.Parameters.AddWithValue("@last_name", txtLastName.Text)
                cmd.Parameters.AddWithValue("@dob", txtDateOfBirth.Text)
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text)
                cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                cmd.Parameters.AddWithValue("@membership_type", cmbMembershipType.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@profile_picture", imgBytes)
                cmd.ExecuteNonQuery()
            End Using

            ' Insert Account
            Dim accountQuery As String = "INSERT INTO accounts (account_number, customer_id, balance, account_type, date_opened, status) 
                                          VALUES (@account_number, @customer_id, @balance, 'Savings', NOW(), 'Active');"
            Using cmd As New MySqlCommand(accountQuery, conn, transaction)
                cmd.Parameters.AddWithValue("@customer_id", customerId)
                cmd.Parameters.AddWithValue("@account_number", accountNumber)
                cmd.Parameters.AddWithValue("@balance", Convert.ToDecimal(txtBalance.Text))
                cmd.ExecuteNonQuery()
            End Using

            transaction.Commit()

            MessageBox.Show("Customer and Account Created Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear Fields
            txtFirstName.Clear()
            txtLastName.Clear()
            txtPhone.Clear()
            txtAddress.Clear()
            txtDateOfBirth.Clear()
            txtBalance.Clear()
            pbProfilePicture.Image = Nothing



        Catch ex As Exception
            If transaction IsNot Nothing Then transaction.Rollback()
            MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub



End Class
