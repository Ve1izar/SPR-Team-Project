import React from 'react';
import { FolderTree, HardDrive, User } from 'lucide-react'; // Іконки для вашого провідника

const MainLayout = ({ children }) => {
  return (
    <div style={{ display: 'flex', height: '100vh', background: '#f9fafb' }}>
      {/* Sidebar - Бічна панель для ієрархії папок */}
      <aside style={{ width: '260px', background: 'white', borderRight: '1px solid #e5e7eb', padding: '20px' }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: '10px', color: '#2563eb', marginBottom: '40px' }}>
          <HardDrive size={28} />
          <h1 style={{ fontSize: '20px', fontWeight: 'bold', margin: 0 }}>LocalDrive</h1>
        </div>
        <nav>
          <div style={{ display: 'flex', alignItems: 'center', gap: '8px', padding: '10px', cursor: 'pointer', borderRadius: '6px' }}>
            <FolderTree size={20} color="#6b7280" />
            <span>Мої Файли</span>
          </div>
        </nav>
      </aside>
      
      {/* Основний контент */}
      <main style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
        <header style={{ height: '64px', background: 'white', borderBottom: '1px solid #e5e7eb', display: 'flex', justifyContent: 'flex-end', alignItems: 'center', padding: '0 30px' }}>
          <User size={24} style={{ cursor: 'pointer' }} />
        </header>
        <section style={{ padding: '30px', overflowY: 'auto' }}>
          {children}
        </section>
      </main>
    </div>
  );
};

export default MainLayout;