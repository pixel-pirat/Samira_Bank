Imports MySql.Data.MySqlClient


Public Class UC_CreateTransaction
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=password1234567890//;database=banking_system;")
    Private Sub UC_CreateTransaction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbTransactionType.Items.AddRange({"Deposit", "Withdrawal", "Transfer", "Loan", "Investment"})
        cmbTransactionType.SelectedIndex = 0
    End Sub

    Private Sub btnSubmitTransaction_Click(sender As Object, e As EventArgs) Handles btnSubmitTransaction.Click
        Dim transactionType As String = cmbTransactionType.SelectedItem.ToString()
        Dim fromAccount As String = txtFromAccount.Text.Trim()
        Dim toAccount As String = txtToAccount.Text.Trim()
        Dim amount As Decimal

        If Not Decimal.TryParse(txtAmount.Text, amount) Then
            MessageBox.Show("Please enter a valid amount.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            conn.Open()

            Select Case transactionType
                Case "Deposit"
                    InsertTransaction(toAccount, "Deposit", amount)

                Case "Withdrawal"
                    InsertTransaction(fromAccount, "Withdrawal", amount)

                Case "Transfer"
                    ' Withdrawal from sender
                    InsertTransaction(fromAccount, "Withdrawal", amount)
                    ' Deposit to receiver
                    InsertTransaction(toAccount, "Deposit", amount)

                Case "Loan"
                    InsertTransaction(toAccount, "Loan", amount)

                Case "Investment"
                    InsertTransaction(toAccount, "Investment", amount)

                Case Else
                    MessageBox.Show("Invalid transaction type selected.")
            End Select

            MessageBox.Show("Transaction processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Optional: Clear fields after transaction
            txtFromAccount.Clear()
            txtToAccount.Clear()
            txtAmount.Clear()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub InsertTransaction(accountNumber As String, transType As String, amount As Decimal)
        Dim query As String = "INSERT INTO transactions (account_number, transaction_type, amount, status) VALUES (@acc, @type, @amount, 'Completed')"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@acc", accountNumber)
            cmd.Parameters.AddWithValue("@type", transType)
            cmd.Parameters.AddWithValue("@amount", amount)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

End Class
