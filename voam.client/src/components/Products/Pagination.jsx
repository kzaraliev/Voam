import styles from "./Products.module.css"

export default function Pagination({ productsPerPage, totalProducts, paginate }) {
  const pageNumbers = [];

    for (let i = 1; i <= Math.ceil(totalProducts / productsPerPage); i++) {
    pageNumbers.push(i);
    }

    if (pageNumbers < 2) {
        return
    }

  return (
    <nav>
      <ul className="pagination" style={{marginTop: "12px", marginBottom: "32px"}}>
        {pageNumbers.map((number) => (
          <li key={number} className="page-item">
            <a onClick={() => paginate(number)} className={styles.pageLink}>
              {number}
            </a>
          </li>
        ))}
      </ul>
    </nav>
  );
}
