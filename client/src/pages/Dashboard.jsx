import React from 'react';
import { Button, Container, Typography, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { removeToken } from '../auth';
// import { loginCustomer, getAccounts } from '../services/api';



export default function Dashboard() {
  const navigate = useNavigate();
  const role = localStorage.getItem("role");
  const username = localStorage.getItem("username");

  const handleLogout = () => {
    removeToken();
    localStorage.removeItem("role");
    localStorage.removeItem("username");
    navigate('/');
  };

  return (
    <Container>
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Typography variant="h4">Välkommen till Bankify{username ? `, ${username}` : ''}</Typography>
        <Button onClick={handleLogout} variant="outlined" color="error">
          Logga ut
        </Button>
      </Box>

      <Button onClick={() => navigate('/accounts')} variant="outlined" sx={{ m: 1 }}>
        Mina konton
      </Button>
      <Button onClick={() => navigate('/transfers')} variant="outlined" sx={{ m: 1 }}>
        Överför pengar
      </Button>

      {role === "Admin" && (
        <Button onClick={() => navigate('/admin')} variant="contained" sx={{ m: 1 }}>
          Adminpanel
        </Button>
      )}
    </Container>
  );
}
