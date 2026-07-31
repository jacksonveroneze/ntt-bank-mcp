const http = require('http');

const PORT = process.env.PORT || 3001;

const server = http.createServer((req, res) => {
    const url = new URL(req.url, `http://localhost:${PORT}/customers/1000`);
    const delayMs = Math.min(
        parseInt(url.searchParams.get('delayMs') ?? '1', 10),
        1_000 // teto de segurança
    );

    setTimeout(() => {
        const customer = {
            customerId: 1000,
            name: "Mariana Silva Santos",
            gender: "Feminino",
            dateOfBirth: new Date("1988-11-23"),
            city: "Capinzal",
            state: "Santa Catarina",
            phone: "+5549991234567",
            email: "mariana.santos@email.com",
            occupation: "Engenheira de Software",
            annualIncome: 115000.00,
            joinDate: new Date("2021-04-15"),
            creditScore: 785
        };

        res.writeHead(200, { 'Content-Type': 'application/json', 'Connection': 'close' });
        res.end(JSON.stringify(customer));
    }, delayMs);
});

server.listen(PORT, () => {
    console.log(`delay-server on http://localhost:${PORT} (GET /?delayMs=N)`);
});