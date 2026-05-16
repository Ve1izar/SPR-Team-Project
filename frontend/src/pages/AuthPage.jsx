import React, { useState } from 'react';

const AuthPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const validate = (e) => {
    e.preventDefault();
    if (!email.includes('@')) {
      setError('Некоректний Email');
    } else if (password.length < 6) {
      setError('Пароль має бути мін. 6 символів');
    } else {
      setError('');
      alert('Успішно! Дані готові для відправки та отримання JWT.');
    }
  };

  return (
    <div style={{ maxWidth: '400px', margin: '50px auto', padding: '30px', background: 'white', borderRadius: '12px', boxShadow: '0 4px 6px -1px rgba(0,0,0,0.1)' }}>
      <h2 style={{ textAlign: 'center', marginBottom: '24px' }}>Вхід</h2>
      <form onSubmit={validate} style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
        <input 
          type="email" 
          placeholder="Електронна пошта" 
          style={{ padding: '12px', border: '1px solid #d1d5db', borderRadius: '6px' }}
          onChange={(e) => setEmail(e.target.value)}
        />
        <input 
          type="password" 
          placeholder="Пароль" 
          style={{ padding: '12px', border: '1px solid #d1d5db', borderRadius: '6px' }}
          onChange={(e) => setPassword(e.target.value)}
        />
        {error && <p style={{ color: '#ef4444', fontSize: '14px', margin: 0 }}>{error}</p>}
        <button type="submit" style={{ padding: '12px', background: '#2563eb', color: 'white', border: 'none', borderRadius: '6px', cursor: 'pointer', fontWeight: 'bold' }}>
          Продовжити
        </button>
      </form>
    </div>
  );
};

export default AuthPage;