import React, { useState, useEffect } from 'react';
import {
  Container,
  TextField,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Typography,
  Box,
  Select,
  MenuItem,
  InputLabel,
  FormControl,
  FormControlLabel,
  Switch
} from '@mui/material';
import { transfer, getAccounts } from '../services/api';

export default function Transfers() {
  const [fromAccount, setFromAccount] = useState('');
  const [toAccount, setToAccount] = useState('');
  const [amount, setAmount] = useState('');
  const [confirmOpen, setConfirmOpen] = useState(false);
  const [accounts, setAccounts] = useState([]);
  const [message, setMessage] = useState('');
  const [toAnotherCustomer, setToAnotherCustomer] = useState(false);

  useEffect(() => {
    const fetchAccounts = async () => {
      try {
        const res = await getAccounts();
        setAccounts(res.data);
      } catch (error) {
        console.error("Kunde inte hämta konton", error);
      }
    };
    fetchAccounts();
  }, []);

  const handleSubmit = () => {
    if (!fromAccount || !toAccount || !amount) {
      setMessage("Fyll i alla fält");
      return;
    }
    setConfirmOpen(true);
  };

  const handleConfirm = async () => {
    setConfirmOpen(false);
    try {
      const res = await transfer({
        fromAccountNumber: fromAccount,
        toAccountNumber: toAccount,
        amount: parseFloat(amount),
        toAnotherCustomer: toAnotherCustomer // om du vill skicka med detta till backend
      });
      setMessage(res.data);
      setFromAccount('');
      setToAccount('');
      setAmount('');
    } catch (error) {
      setMessage("Överföringen misslyckades. Kontrollera uppgifterna.");
    }
  };

  return (
    <Container sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>Överför pengar</Typography>

      <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, maxWidth: 400 }}>
        <FormControl fullWidth>
          <InputLabel>Från konto</InputLabel>
          <Select
            value={fromAccount}
            label="Från konto"
            onChange={(e) => setFromAccount(e.target.value)}
          >
            {accounts.map((acc) => (
              <MenuItem key={acc.accountNumber} value={acc.accountNumber}>
                {acc.accountNumber} — {acc.balance} kr
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <FormControlLabel
          control={
            <Switch
              checked={toAnotherCustomer}
              onChange={(e) => setToAnotherCustomer(e.target.checked)}
              color="primary"
            />
          }
          label="Till annan kund"
        />

        {toAnotherCustomer ? (
          <TextField
            label="Till kontonummer (annan kund)"
            value={toAccount}
            onChange={(e) => setToAccount(e.target.value)}
            fullWidth
          />
        ) : (
          <FormControl fullWidth>
            <InputLabel>Till konto</InputLabel>
            <Select
              value={toAccount}
              label="Till konto"
              onChange={(e) => setToAccount(e.target.value)}
            >
              {accounts
                .filter(acc => acc.accountNumber !== fromAccount)
                .map((acc) => (
                  <MenuItem key={acc.accountNumber} value={acc.accountNumber}>
                    {acc.accountNumber} — {acc.balance} kr
                  </MenuItem>
                ))}
            </Select>
          </FormControl>
        )}

        <TextField
          label="Belopp"
          type="number"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
          fullWidth
        />

        <Button variant="contained" color="primary" onClick={handleSubmit}>
          Överför
        </Button>

        {message && (
          <Typography sx={{ mt: 2 }} color="secondary">{message}</Typography>
        )}
      </Box>

      <Dialog open={confirmOpen} onClose={() => setConfirmOpen(false)}>
        <DialogTitle>Bekräfta överföring</DialogTitle>
        <DialogContent>
          <Typography>
            Vill du överföra <strong>{amount} kr</strong> från <strong>{fromAccount}</strong> till <strong>{toAccount}</strong>?
          </Typography>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setConfirmOpen(false)}>Avbryt</Button>
          <Button variant="contained" color="primary" onClick={handleConfirm}>Bekräfta</Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
}
