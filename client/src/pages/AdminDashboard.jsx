import React from 'react';
import { useNavigate } from 'react-router-dom';

const AdminDashboard = () => {
  const navigate = useNavigate();

  return (
    <div className="p-8 space-y-6">
      <h1 className="text-2xl font-bold">Adminpanel</h1>
      <div className="space-y-4">
        <button 
          onClick={() => navigate('/create-customer')} 
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 w-full"
        >
          🧍 Skapa ny kund
        </button>
        <button 
          onClick={() => navigate('/new-loan')} 
          className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700 w-full"
        >
          💸 Skapa nytt lån
        </button>
      </div>
    </div>
  );
};

export default AdminDashboard;
