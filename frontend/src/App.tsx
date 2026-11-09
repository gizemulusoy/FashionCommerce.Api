import { useEffect, useState } from 'react'
import './App.css'

// Describe the product data returned by the API.
type Product = {
    id: number
    name: string
    price: number
}

function App() {
    const [products, setProducts] = useState<Product[]>([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')

    useEffect(() => {
        const controller = new AbortController()

        // Load products from the backend.
        async function loadProducts() {
            try {
                const response = await fetch(
                    'https://localhost:7107/api/products',
                    { signal: controller.signal },
                )

                if (!response.ok) {
                    throw new Error('Could not load products.')
                }

                const data: Product[] = await response.json()
                setProducts(data)
            } catch {
                if (!controller.signal.aborted) {
                    setError('Could not load products. Please try again.')
                }
            } finally {
                if (!controller.signal.aborted) {
                    setLoading(false)
                }
            }
        }

        loadProducts()

        // Cancel the request when this effect is cleaned up.
        return () => controller.abort()
    }, [])

    return (
        <main>
            <h1>FashionCommerce</h1>
            <p>Discover your everyday style.</p>

            <h2>Our Products</h2>

            {loading && <p>Loading products...</p>}
            {error && <p role="alert">{error}</p>}

            {products.map((product) => (
                <article key={product.id}>
                    <h3>{product.name}</h3>
                    <p>Price: {product.price.toFixed(2)} TRY</p>
                </article>
            ))}
        </main>
    )
}

export default App