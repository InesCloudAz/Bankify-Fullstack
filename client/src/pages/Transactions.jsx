import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getTransactions } from '../services/api';
import { Container, Typography, CircularProgress, List, ListItem, ListItemText } from '@mui/material';

export default function Transactions() {
  const { accountNumber } = useParams(); // Hämta kontonumret från URL
  const [transactions, setTransactions] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchTransactions = async () => {
      try {
        const res = await getTransactions(accountNumber); // Hämta transaktioner från API
        setTransactions(res.data);
      } catch (err) {
        console.error('Kunde inte hämta transaktioner', err);
      } finally {
        setLoading(false);
      }
    };

    fetchTransactions();
  }, [accountNumber]); // Hämtar transaktioner när kontonumret ändras

  if (loading) {
    return <Container sx={{ textAlign: 'center', mt: 5 }}><CircularProgress /></Container>;
  }

  return (
    <Container sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>Transaktioner för Konto: {accountNumber}</Typography>

      {transactions.length === 0 ? (
        <Typography>Inga transaktioner hittades för detta konto.</Typography>
      ) : (
        <List>
          {transactions.map((transaction, index) => (
            <ListItem key={index}>
              <ListItemText
                primary={`Belopp: ${transaction.amount} kr`}
                secondary={`Datum: ${new Date(transaction.date).toLocaleDateString("sv-SE")}`}
              />
            </ListItem>
          ))}
        </List>
      )}
    </Container>
  );
}
