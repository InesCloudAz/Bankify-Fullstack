import axios from 'axios';
import { getToken } from '../auth';

const API = axios.create({
  baseURL: 'http://localhost:5000/api', 
});

API.interceptors.request.use((config) => {
  const token = getToken();
  console.log("Token som skickas:", token);
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

// Customer login (för kunder)
export const loginCustomer = async (credentials) => {
  console.log("Skickar credentials:", credentials);
  try {
    const response = await API.post('/customer/login', credentials);
    console.log("Respons från backend:", response.data);
    return response;
  } catch (error) {
    console.error("Fel vid login:", error.response?.data || error.message);
    throw error;
  }
};

// Admin login (för admin)
export const loginUser = async (credentials) => {
  console.log("Skickar credentials:", credentials);
  try {
    const response = await API.post('/user/login', credentials);
    console.log("Respons från backend:", response.data);
    return response;
  } catch (error) {
    console.error("Fel vid login:", error.response?.data || error.message);
    throw error;
  }
};

// Hämta alla konton för kund
export const getAccounts = () => API.get('/customer/accounts');

// Hämta transaktioner för kundens konto
export const getTransactions = (accountNumber) => 
  API.get(`/customer/transactions/by-account-number/${accountNumber}`);

// Skapa nytt konto för kund
export const createAccount = (data) => API.post('/customer/create-account', data);

// Gör en överföring mellan kundens egna konton
export const transfer = (data) => API.post('/customer/transfer-between-accounts', data);

// Gör en överföring till en annan kund
export const transferToAnotherCustomer = (data) =>
  API.post('/customer/transfer-to-another-customer', data);

// Skapa en ny kund (adminfunktion)
export const createNewCustomer = (customerData, accountType = 'Standard transaction account') =>
  API.post(`/user/NewCustomer?accountTypeName=${encodeURIComponent(accountType)}`, customerData);

// Skapa ett nytt lån (adminfunktion)
export const createNewLoan = (loanData) => 
  API.post('/user/NewLoan', loanData);
