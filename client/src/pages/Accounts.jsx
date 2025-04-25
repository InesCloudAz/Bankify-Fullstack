import React, { useEffect, useState } from 'react';
import { Card, CardContent, Typography, Grid, Container, CircularProgress, Button } from '@mui/material';
import { getAccounts } from '../services/api';
import { Link } from 'react-router-dom'; // Importera Link för att skapa länkar

export default function Accounts() {
  const [accounts, setAccounts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchAccounts = async () => {
      try {
        const res = await getAccounts();
        setAccounts(res.data);
      } catch (err) {
        console.error('Kunde inte hämta konton', err);
      } finally {
        setLoading(false);
      }
    };

    fetchAccounts();
  }, []);

  if (loading) {
    return <Container sx={{ textAlign: 'center', mt: 5 }}><CircularProgress /></Container>;
  }

  return (
    <Container sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>Mina konton</Typography>
      <Grid container spacing={3}>
        {accounts.map((account) => (
          <Grid item xs={12} md={6} lg={4} key={account.accountID}>
            <Card>
              <CardContent>
                <Typography variant="h6">Kontonummer: {account.accountNumber}</Typography>
                <Typography>Saldo: {account.balance} kr</Typography>
                <Typography>Typ: {account.typeName}</Typography>
                <Typography>Frekvens: {account.frequency}</Typography>
                <Typography>Skapad: {new Date(account.created).toLocaleDateString("sv-SE")}</Typography>

                {/* Lägg till länk för att visa transaktioner för det här kontot */}
                <Link to={`/transactions/${account.accountNumber}`} style={{ textDecoration: 'none' }}>
                
                  <Button variant="contained" color="primary" sx={{ mt: 2 }}>
                    Visa Transaktioner
                  </Button>
                </Link>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Container>
  );
}
