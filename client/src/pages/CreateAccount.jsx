import React, { useState } from 'react';
import { TextField, Button, Container, Typography, MenuItem, Alert } from '@mui/material';
import { createAccount } from '../services/api';

const accountTypes = [
  'Standard transaction account',
  'Savings account',
  'Business account',
  "Children's account",
];

const frequencies = ['Monthly', 'Weekly', 'Yearly'];

export default function CreateAccount() {
  const [typeName, setTypeName] = useState('');
  const [initialDeposit, setInitialDeposit] = useState('');
  const [frequency, setFrequency] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const res = await createAccount({ typeName, initialDeposit: Number(initialDeposit), frequency });
      setSuccessMessage(res.data.message);
      setError('');
      setTypeName('');
      setInitialDeposit('');
      setFrequency('');
    } catch (err) {
      setError(err.response?.data || 'Något gick fel.');
      setSuccessMessage('');
    }
  };

  return (
    <Container sx={{ mt: 4, maxWidth: 'sm' }}>
      <Typography variant="h4" gutterBottom>Skapa nytt konto</Typography>

      {successMessage && <Alert severity="success" sx={{ mb: 2 }}>{successMessage}</Alert>}
      {error && <Alert severity="error" sx={{ mb: 2 }}>{error}</Alert>}

      <form onSubmit={handleSubmit}>
        <TextField
          select
          fullWidth
          label="Kontotyp"
          value={typeName}
          onChange={(e) => setTypeName(e.target.value)}
          sx={{ mb: 2 }}
          required
        >
          {accountTypes.map((type) => (
            <MenuItem key={type} value={type}>{type}</MenuItem>
          ))}
        </TextField>

        <TextField
          fullWidth
          type="number"
          label="Startbelopp"
          value={initialDeposit}
          onChange={(e) => setInitialDeposit(e.target.value)}
          sx={{ mb: 2 }}
          required
        />

        <TextField
          select
          fullWidth
          label="Frekvens"
          value={frequency}
          onChange={(e) => setFrequency(e.target.value)}
          sx={{ mb: 2 }}
          required
        >
          {frequencies.map((freq) => (
            <MenuItem key={freq} value={freq}>{freq}</MenuItem>
          ))}
        </TextField>

        <Button type="submit" variant="contained" color="primary" fullWidth>
          Skapa konto
        </Button>
      </form>
    </Container>
  );
}
