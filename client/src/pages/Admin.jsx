import React, { useState } from 'react';
import { Container, TextField, Button, Typography, MenuItem } from '@mui/material';
import { createNewCustomer } from '../services/api';

const accountTypes = [
  "Standard transaction account",
  "Savings account",
  "Business Account",
  "Children's Account"
];

export default function Admin() {
  const [customer, setCustomer] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    address: '',
    ssn: '',
  });

  const [accountType, setAccountType] = useState(accountTypes[0]);
  const [response, setResponse] = useState(null);

  const handleChange = (e) =>
    setCustomer({ ...customer, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const res = await createNewCustomer(customer, accountType);
      setResponse(res.data);
    } catch (err) {
      console.error("Fel vid skapande:", err);
      alert("Kunde inte skapa ny kund.");
    }
  };

  return (
    <Container sx={{ mt: 4 }}>
      <Typography variant="h5" gutterBottom>Skapa ny kund</Typography>

      <form onSubmit={handleSubmit}>
        {["firstName", "lastName", "email", "phoneNumber", "address", "ssn"].map((field) => (
          <TextField
            key={field}
            name={field}
            label={field.charAt(0).toUpperCase() + field.slice(1)}
            value={customer[field]}
            onChange={handleChange}
            fullWidth
            margin="normal"
          />
        ))}

        <TextField
          select
          label="Kontotyp"
          value={accountType}
          onChange={(e) => setAccountType(e.target.value)}
          fullWidth
          margin="normal"
        >
          {accountTypes.map((type) => (
            <MenuItem key={type} value={type}>
              {type}
            </MenuItem>
          ))}
        </TextField>

        <Button type="submit" variant="contained" sx={{ mt: 2 }}>
          Skapa kund
        </Button>
      </form>

      {response && (
        <Typography sx={{ mt: 2 }} color="green">
          {response.Message}<br />
          Användarnamn: <b>{response.Username}</b><br />
          Lösenord: <b>{response.Password}</b><br />
          Konto: <b>{response.AccountNumber}</b>
        </Typography>
      )}
    </Container>
  );
}
