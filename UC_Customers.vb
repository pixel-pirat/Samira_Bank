Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient

Public Class UC_Customers
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=password1234567890//;database=banking_system;")
    Public Property YourStatusColumnIndex As Integer


    Private Sub UC_Customers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerData()
    End Sub

    Private Sub LoadCustomerData()
        Try
            conn.Open()
            Dim query As String = "SELECT * FROM customers"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim table As New DataTable()
            adapter.Fill(table)

            ' Bind data to DataGridView
            DataGridView1.DataSource = table
        Catch ex As Exception
            MessageBox.Show("Error loading customer data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Guna2Button11_Click(sender As Object, e As EventArgs) Handles Guna2Button11.Click
        Dim parentForm = CType(Me.ParentForm, Form3)
        parentForm.LoadUserControl(New UC_AddCustomer())
    End Sub


    ' End Sub
End Class
