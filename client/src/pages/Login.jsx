import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { loginCustomer, loginUser } from '../services/api';
import { setToken } from '../auth';


const Login = () => {
  const navigate = useNavigate();
  const [credentials, setCredentials] = useState({ username: '', password: '' });
  const [isAdmin, setIsAdmin] = useState(false); // checkbox: "Är du admin?"
  const [error, setError] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    try {
      const response = isAdmin ? await loginUser(credentials) : await loginCustomer(credentials);
      const token = response.data.token;

      setToken(token);

      // Hämta roll från JWT-token
      const payload = JSON.parse(atob(token.split('.')[1]));
      const role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

      if (role === "Admin") {
        navigate('/admin-dashboard');
      } else {
        navigate('/');
      }
    } catch (err) {
      setError(err.response?.data || 'Något gick fel vid inloggning.');
    }
  };

  return (
    <div className="p-8 max-w-md mx-auto">
      <h2 className="text-xl font-bold mb-4">Logga in</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
        <input
          type="text"
          placeholder="Användarnamn"
          value={credentials.username}
          onChange={(e) => setCredentials({ ...credentials, username: e.target.value })}
          className="border p-2 w-full"
        />
        <input
          type="password"
          placeholder="Lösenord"
          value={credentials.password}
          onChange={(e) => setCredentials({ ...credentials, password: e.target.value })}
          className="border p-2 w-full"
        />
        <label className="flex items-center space-x-2">
          <input
            type="checkbox"
            checked={isAdmin}
            onChange={(e) => setIsAdmin(e.target.checked)}
          />
          <span>Är du admin?</span>
        </label>
        {error && <div className="text-red-500">{error}</div>}
        <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 w-full">
          Logga in
        </button>
      </form>
    </div>
  );
};

export default Login;
