import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import 'bootstrap/dist/css/bootstrap.min.css' // ЦЕЙ РЯДОК ОБОВ'ЯЗКОВИЙ
import 'bootstrap-icons/font/bootstrap-icons.css' // І ЦЕЙ ТЕЖ

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <App />
    </React.StrictMode>,
)