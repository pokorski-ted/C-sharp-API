import { useEffect, useState } from "react";

export default function App() {
    const [status, setStatus] = useState("Starting...");
    const [products, setProducts] = useState([]);

    useEffect(() => {
        async function load() {
            try {
                const baseUrl = import.meta.env.VITE_API_BASE_URL;

                if (!baseUrl) {
                    setStatus("Missing VITE_API_BASE_URL in .env");
                    return;
                }

                setStatus("Calling API...");
                const res = await fetch(`${baseUrl}/api/products`);

                if (!res.ok) {
                    setStatus(`API call failed: HTTP ${res.status}`);
                    return;
                }

                const data = await res.json();
                setProducts(data);
                setStatus(`Loaded ${data.length} products.`);
            } catch (err) {
                setStatus(`Error: ${err.message}`);
            }
        }

        load();
    }, []);

    return (
        <div style={{ padding: 16 }}>
            <h1>Products UI (React)</h1>
            <p><strong>Status:</strong> {status}</p>

            <h2>Products</h2>
            <ul>
                {products.map((p) => (
                    <li key={p.id}>
                        {p.id}: {p.name}
                    </li>
                ))}
            </ul>
        </div>
    );
}

