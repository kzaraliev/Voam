import {
  Chart,
  CategoryScale,
  LinearScale,
  BarElement,
  ArcElement,
  Tooltip,
  Legend,
} from "chart.js";
Chart.register(
  CategoryScale,
  LinearScale,
  BarElement,
  ArcElement,
  Tooltip,
  Legend
);
import { Bar, Pie } from "react-chartjs-2";
import styles from "./Admin.module.css";

const options = {
  scales: {
    y: {
      beginAtZero: true,
      ticks: {
        stepSize: 1,
        callback: function (value) {
          if (value % 1 === 0) {
            return value;
          }
        },
      },
    },
  },
};

export default function Charts({ orders }) {
  //Orders by month - Bar
  const ordersByMonth = orders.reduce((acc, order) => {
    const monthYear = new Date(
      order.orderDate.split(".").reverse().join("-")
    ).toLocaleString("default", { month: "long", year: "numeric" });
    if (!acc[monthYear]) {
      acc[monthYear] = 0;
    }
    acc[monthYear]++;
    return acc;
  }, {});

  const ordersByMonthData = {
    labels: Object.keys(ordersByMonth),
    datasets: [
      {
        label: "Total Orders per Month",
        data: Object.values(ordersByMonth),
        backgroundColor: generateColors(Object.keys(ordersByMonth).length),
      },
    ],
  };

  //Ratio of products sold - Pie
  const productCounts = orders
    .flatMap((order) => order.products)
    .reduce((acc, product) => {
      // Using product name as the key; you can change this to product.id if preferable
      const key = product.name;
      if (!acc[key]) {
        acc[key] = 0;
      }
      acc[key] += product.quantity; // Increment by quantity
      return acc;
    }, {});

  const ratioProductsData = {
    labels: Object.keys(productCounts),
    datasets: [
      {
        data: Object.values(productCounts),
        backgroundColor: generateColors(Object.keys(productCounts).length),
      },
    ],
  };

  // Sales during the days of the week - Bar
  const ordersByDay = {
    Monday: 0,
    Tuesday: 0,
    Wednesday: 0,
    Thursday: 0,
    Friday: 0,
    Saturday: 0,
    Sunday: 0,
  };

  orders.forEach((order) => {
    const dayOfWeek = new Date(
      order.orderDate.split(".").reverse().join("-")
    ).toLocaleString("en-US", { weekday: "long" });
    ordersByDay[dayOfWeek]++;
  });

  const ordersByWeekDayData = {
    labels: Object.keys(ordersByDay),
    datasets: [
      {
        label: "Orders by Day of Week",
        data: Object.values(ordersByDay),
        backgroundColor: generateColors(Object.keys(ordersByDay).length),
      },
    ],
  };

  return (
    <div className={styles.chartsSquare}>
      <div className={styles.chart}>
        <p className={styles.totalOrders}>Total orders</p>
        <h4 className={styles.totalOrdersNumber}>{orders.length}</h4>
      </div>
      <Pie className={styles.chart} data={ratioProductsData} />
      <Bar
        className={styles.chart}
        data={ordersByMonthData}
        options={options}
      />
      <Bar
        className={styles.chart}
        data={ordersByWeekDayData}
        options={options}
      />
    </div>
  );
}

function generateColors(n) {
  const colors = [];
  for (let i = 0; i < n; i++) {
    const hue = Math.round((360 / n) * i);
    colors.push(`hsl(${hue}, 100%, 75%)`);
  }
  return colors;
}
