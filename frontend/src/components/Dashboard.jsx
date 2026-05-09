import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Dashboard = () => {
    const [files, setFiles] = useState([]);
    const [uploading, setUploading] = useState(false);

    const api = axios.create({ baseURL: 'http://localhost:5000/api/files' });

    const getFiles = async () => {
        try {
            const res = await api.get('/');
            setFiles(res.data);
        } catch (err) { console.error(err); }
    };

    useEffect(() => { getFiles(); }, []);

    const handleUpload = async (e) => {
        const file = e.target.files[0];
        if (!file) return;

        const formData = new FormData();
        formData.append('file', file);

        setUploading(true);
        try {
            await api.post('/upload', formData);
            getFiles();
        } catch (err) { alert("Помилка"); }
        finally { setUploading(false); }
    };

    const handleDownload = (fileName) => {
        window.open(`http://localhost:5000/api/files/download/${fileName}`, '_blank');
    };

    return (
        <div className="container-fluid p-4">
            <nav className="navbar navbar-dark bg-dark fixed-top shadow-sm px-4">
                <span className="navbar-brand fw-bold">Local Drive - Team 3</span>
            </nav>

            <div className="mt-5 pt-4">
                <div className="d-flex justify-content-between align-items-center mb-4 border-bottom pb-3">
                    <h2 className="fw-bold m-0">Мій Диск</h2>
                    <label className={`btn btn-primary px-4 ${uploading ? 'disabled' : ''}`}>
                        <i className={`bi ${uploading ? 'bi-hourglass-split' : 'bi-plus-lg'} me-2`}></i>
                        {uploading ? 'Завантаження...' : 'Додати файл'}
                        <input type="file" hidden onChange={handleUpload} />
                    </label>
                </div>

                <div className="row row-cols-2 row-cols-md-4 row-cols-lg-6 g-3">
                    {files.map((file, idx) => (
                        <div className="col" key={idx}>
                            <div className="card h-100 border-0 shadow-sm text-center py-3">
                                <div className="card-body">
                                    <i className="bi bi-file-earmark-text text-primary display-4 mb-2"></i>
                                    <div className="text-truncate fw-bold mb-1 px-2">{file.name}</div>
                                    <div className="text-muted small mb-3">{file.size}</div>
                                    <button
                                        className="btn btn-outline-primary btn-sm w-100"
                                        onClick={() => handleDownload(file.name)}
                                    >
                                        Скачати
                                    </button>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>

                {files.length === 0 && !uploading && (
                    <div className="text-center py-5 text-muted">
                        <i className="bi bi-folder2-open display-1 opacity-25"></i>
                        <p className="mt-3">Папка порожня</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Dashboard;